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
        public bool UseInitialPositionsAsSpawnPoints = true;
        /// <summary>
        /// Additional SpawnPoints
        /// </summary>
        public List<SpawnPoint> SpawnPoints;

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
            agentsPrefb = Resources.LoadAll<GameObject>("Prefabs/Agents");
            Agent.OnAgentKilled += HandleOnAgentKilled;
            if(UseInitialPositionsAsSpawnPoints)
            {
                foreach (Agent agent in FindObjectsOfType<Agent>())
                {
                    SpawnPoint newPos;
                    newPos.SpawnPosition = Instantiate(new GameObject("SpawnPoint_"+ agent.name), agent.transform.position, agent.transform.rotation, this.transform).transform;
                    newPos.PlayerIndx = agent.playerIndex;

                    OriginalSpawns.Add(newPos);
                }
            }

            if(SpawnPoints != null)
                foreach (SpawnPoint spwnPt in SpawnPoints)
                {
                    OriginalSpawns.Add(spwnPt);
                }            
        }

        protected override void OnDeactivation()
        {
            Agent.OnAgentKilled -= HandleOnAgentKilled;
        }
        #endregion

        #region API
        /// <summary>
        /// Respawn all Players
        /// </summary>
        public void RespawnAllImmediate()
        {
            for (int i = 0; i < agentsPrefb.Length; i++)
            {
                ///RespawnImmediate(agentsPrefb[i].GetComponent<Agent>().playerIndex);
            }
        }
        /// <summary>
        /// Respawn a Player without cooldown
        /// </summary>
        /// <param name="_playerIndx">Player to spawn</param>
        public void RespawnImmediate(Agent _agent)
        {
            //Prevent double istance
            //foreach (Agent agnt in FindObjectsOfType<Agent>())
            //{
            //    if (agnt.playerIndex == _playerIndx)
            //        Destroy(agnt);
            //}
            
            //TODO: sostituire la lista SpawnPoint nel successivo foreach
            //con una lista che prevede il corretto criterio di selezione degli spawn points.
            foreach (SpawnPoint spawn in OriginalSpawns)
            {
                if (spawn.PlayerIndx == _agent.playerIndex)
                {
                    _agent.gameObject.transform.position = spawn.SpawnPosition.position;
                    _agent.gameObject.transform.rotation = spawn.SpawnPosition.rotation;
                    _agent.Init();
                }
            }
        }
        /// <summary>
        /// Respawn after a fixed amount of time
        /// </summary>
        /// <param name="_playerIndx">Player to spawn</param>
        public void RespawnAvatar(Agent _agent)
        {
            StartCoroutine("RespawnCooldown", _agent);            
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
            RespawnAvatar(_victim);
        }
        #endregion

        [System.Serializable]
        public struct SpawnPoint
        {
            public Transform SpawnPosition;
            public PlayerIndex PlayerIndx;
        }

        IEnumerator RespawnCooldown(Agent _agent)
        {
            yield return new WaitForSeconds(RespawnTime);
            RespawnImmediate(_agent);
        }
    }
}
