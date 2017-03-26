using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    /// <summary>
    /// It menages the SpawnPoint position during the Level
    /// </summary>
    public class AvatarSpawner : SpawnerBase
    {
        /// <summary>
        /// Time between death and respawn
        /// </summary>
        public float RespawnTime = 0;
        /// <summary>
        /// Use the initial position as SpawnPoints
        /// </summary>
        public bool UseInitialPositionsAsSpawnPoints = false;
        /// <summary>
        /// Additional SpawnPoints
        /// </summary>
        public List<SpawnPoint> SpawnPoints;
        /// <summary>
        /// Use the Specifiied prefabas as player to respawn
        /// </summary>
        public bool UseSpecifiedPrefabs = false;
        /// <summary>
        /// Prefabs of Avatar to respawn
        /// </summary>
        public GameObject[] AvatarPrefabs = new GameObject[4];

        private List<SpawnPoint> _originalSpawns;
        public List<SpawnPoint> OriginalSpawns
        {
            get
            {
                if (_originalSpawns == null)
                    _originalSpawns = new List<SpawnPoint>();
                return _originalSpawns;
            }
            set { _originalSpawns = value; }
        }
        private GameObject[] agentsPrefb;

        #region Spawner Life flow
        /// <summary>
        /// Save the desired SpawnPoints
        /// </summary>
        protected override void OnActivation()
        {
            if (UseSpecifiedPrefabs)
                agentsPrefb = AvatarPrefabs;
            else
                agentsPrefb = Resources.LoadAll<GameObject>("Prefabs/Agents");

            EventManager.OnAgentKilled += HandleOnAgentKilled;
            if (UseInitialPositionsAsSpawnPoints)
            {
                foreach (Agent agent in FindObjectsOfType<Agent>())
                {
                    SpawnPoint newPos;
                    GameObject newSpawn = new GameObject("SpawnPoint_" + agent.name);
                    newSpawn.transform.position = agent.transform.position;
                    newSpawn.transform.rotation = agent.transform.rotation;
                    newSpawn.transform.parent = transform;

                    newPos.SpawnPosition = newSpawn.transform;
                    newPos.PlayerIndx = agent.playerIndex;

                    OriginalSpawns.Add(newPos);
                }
            }

            if (SpawnPoints != null)
                foreach (SpawnPoint spwnPt in SpawnPoints)
                {
                    OriginalSpawns.Add(spwnPt);
                }
        }

        protected override void OnDeactivation()
        {
            EventManager.OnAgentKilled -= HandleOnAgentKilled;
        }
        #endregion

        #region API
        /// <summary>
        /// Respawn all Players
        /// </summary>
        public void RespawnAllImmediate()
        {
            StopAllCoroutines();
            for (int i = 0; i < agentsPrefb.Length; i++)
            {
                RespawnImmediate(agentsPrefb[i].GetComponentInChildren<Agent>().playerIndex);
            }
        }

        /// <summary>
        /// Respawn a Player without cooldown
        /// </summary>
        /// <param name="_playerIndx">Player to spawn</param>
        public void RespawnImmediate(PlayerIndex _playerIndx)
        {
            //Prevent double istance
            foreach (Agent agnt in FindObjectsOfType<Agent>())
            {
                if (agnt.playerIndex == _playerIndx)
                    Destroy(agnt);
            }

            //TODO: sostituire la lista SpawnPoint nel successivo foreach
            //con una lista che prevede il corretto criterio di selezione degli spawn points.
            foreach (SpawnPoint spawn in OriginalSpawns)
            {
                if (spawn.PlayerIndx == _playerIndx)
                {
                    for (int i = 0; i < agentsPrefb.Length; i++)
                    {
                        if (agentsPrefb[i].GetComponentInChildren<Agent>().playerIndex == _playerIndx)
                        {
                            GameObject newAgent = Instantiate(agentsPrefb[i], spawn.SpawnPosition.position, spawn.SpawnPosition.rotation);
                            if(EventManager.OnAgentSpawn != null)
                                EventManager.OnAgentSpawn(newAgent.GetComponentInChildren<Agent>());
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Respawn after a fixed amount of time
        /// </summary>
        /// <param name="_playerIndx">Player to spawn</param>
        public void RespawnAvatar(PlayerIndex _playerIndx)
        {
            StartCoroutine("RespawnCooldown", _playerIndx);
        }
        #endregion

        #region Event
        /// <summary>
        /// Respawn Agent on Death
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        void HandleOnAgentKilled(Agent _killer, Agent _victim)
        {
            RespawnAvatar(_victim.playerIndex);
        }
        #endregion

        [System.Serializable]
        public struct SpawnPoint
        {
            public Transform SpawnPosition;
            public PlayerIndex PlayerIndx;
        }

        IEnumerator RespawnCooldown(PlayerIndex _playerIndx)
        {
            yield return new WaitForSeconds(RespawnTime);
            RespawnImmediate(_playerIndx);
        }
    }
}
