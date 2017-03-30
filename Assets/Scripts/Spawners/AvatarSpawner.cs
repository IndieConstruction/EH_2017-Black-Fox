using System;
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
        new public AvatarSpawnerOptions Options;

        private List<AvatarSpawnPoint> _originalSpawns;
        public List<AvatarSpawnPoint> OriginalSpawns
        {
            get
            {
                if (_originalSpawns == null)
                    _originalSpawns = new List<AvatarSpawnPoint>();
                return _originalSpawns;
            }
            set { _originalSpawns = value; }
        }
        /// <summary>
        /// Additional SpawnPoints
        /// </summary>
        public List<AvatarSpawnPoint> SpawnPoints;
        private GameObject[] agentsPrefb;
        
        /// <summary>
        /// Save the desired SpawnPoints
        /// </summary>
        void Start()
        {
            if (Options.UseSpecifiedPrefabs)
                agentsPrefb = Options.AvatarPrefabs;
            else
                agentsPrefb = Resources.LoadAll<GameObject>("Prefabs/Agents");
           
            if (SpawnPoints != null)
                foreach (AvatarSpawnPoint spwnPt in SpawnPoints)
                {
                    OriginalSpawns.Add(spwnPt);
                }

            foreach (SpawnPoint spawn in FindObjectsOfType<SpawnPoint>())
            {
                AvatarSpawnPoint spwnPt;
                switch (spawn.SpawnAvatar)
                {
                    case SpawnPoint.AvatarSpawnType.None:
                        break;
                    case SpawnPoint.AvatarSpawnType.Blue:
                        spwnPt.SpawnPosition = spawn.transform;
                        spwnPt.PlayerIndx = PlayerIndex.One;
                        OriginalSpawns.Add(spwnPt);
                        break;
                    case SpawnPoint.AvatarSpawnType.Red:
                        spwnPt.SpawnPosition = spawn.transform;
                        spwnPt.PlayerIndx = PlayerIndex.Two;
                        OriginalSpawns.Add(spwnPt);
                        break;
                    case SpawnPoint.AvatarSpawnType.Green:
                        spwnPt.SpawnPosition = spawn.transform;
                        spwnPt.PlayerIndx = PlayerIndex.Three;
                        OriginalSpawns.Add(spwnPt);
                        break;
                    case SpawnPoint.AvatarSpawnType.Purple:
                        spwnPt.SpawnPosition = spawn.transform;
                        spwnPt.PlayerIndx = PlayerIndex.Four;
                        OriginalSpawns.Add(spwnPt);
                        break;
                    default:
                        break;
                }

            }
        }

        public override SpawnerBase Init(SpawnerOptions options) {
            Options = options as AvatarSpawnerOptions;
            return this;
        }

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
            foreach (AvatarSpawnPoint spawn in OriginalSpawns)
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
        /// Respawn Agent on Death
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        public void ReactToOnAgentKilled(Agent _victim)
        {
            RespawnAvatar(_victim.playerIndex);
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

        [Serializable]
        public struct AvatarSpawnPoint
        {
            public Transform SpawnPosition;
            public PlayerIndex PlayerIndx;
        }

        IEnumerator RespawnCooldown(PlayerIndex _playerIndx)
        {
            yield return new WaitForSeconds(Options.RespawnTime);
            RespawnImmediate(_playerIndx);
        }
    }

    [System.Serializable]
    public class AvatarSpawnerOptions : SpawnerOptions {
        /// <summary>
        /// Time between death and respawn
        /// </summary>
        public float RespawnTime = 0;

        /// <summary>
        /// Use the Specifiied prefabas as player to respawn
        /// </summary>
        public bool UseSpecifiedPrefabs = false;
        /// <summary>
        /// Prefabs of Avatar to respawn
        /// </summary>
        public GameObject[] AvatarPrefabs = new GameObject[4];
    }
}
