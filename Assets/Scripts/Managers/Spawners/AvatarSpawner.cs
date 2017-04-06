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
    public class AvatarSpawner : MonoBehaviour
    {
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
            if (UseSpecifiedPrefabs)
                agentsPrefb = AvatarPrefabs;
            else
                agentsPrefb = Resources.LoadAll<GameObject>("Prefabs/Avatar");
           
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

        #region API
        public Avatar[] GetAllPlayer()
        {
            // TODO : tenere riferimenti fissi
            return FindObjectsOfType<Avatar>();
        }

        /// <summary>
        /// Respawn all Players
        /// </summary>
        public void RespawnAllImmediate()
        {
            StopAllCoroutines();
            for (int i = 0; i < agentsPrefb.Length; i++)
            {
                RespawnImmediate(agentsPrefb[i].GetComponentInChildren<Avatar>().playerIndex);
            }
        }

        /// <summary>
        /// Respawn a Player without cooldown
        /// </summary>
        /// <param name="_playerIndx">Player to spawn</param>
        public void RespawnImmediate(PlayerIndex _playerIndx)
        {
            //Prevent double istance
            foreach (Avatar agnt in FindObjectsOfType<Avatar>())
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
                        if (agentsPrefb[i].GetComponentInChildren<Avatar>().playerIndex == _playerIndx)
                        {
                            GameObject newAgent = Instantiate(agentsPrefb[i], spawn.SpawnPosition.position, spawn.SpawnPosition.rotation);
                            if (GameManager.Instance.LevelMng.RopeMng != null)
                                GameManager.Instance.LevelMng.RopeMng.AttachNewRope(newAgent.GetComponentInChildren<Avatar>());
                            if(EventManager.OnAgentSpawn != null)
                                EventManager.OnAgentSpawn(newAgent.GetComponentInChildren<Avatar>());
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
        public void RespawnAvatar(Avatar _victim)
        {
            StartCoroutine("RespawnCooldown", _victim.playerIndex);
        }

        #region Destroy Agents
        public void DestroyAgents() {
            // TODO : da correggere
            foreach (Avatar agent in FindObjectsOfType<Avatar>()) {
                Destroy(agent.gameObject);
            }
        }
        #endregion

        #endregion

        [Serializable]
        public struct AvatarSpawnPoint
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
