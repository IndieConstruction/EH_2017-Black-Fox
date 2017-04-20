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
        private GameObject avatarContainer;
        
        private List<AvatarSpawnPoint> _originalSpawns;
        /// <summary>
        /// Rende delle transform specifiche utilizzabili come spawn point per player specifico
        /// </summary>
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
        
        /// <summary>
        /// Save the desired SpawnPoints
        /// </summary>
        void Start()
        {           
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
                        spwnPt.PlayerID = PlayerLabel.One;
                        OriginalSpawns.Add(spwnPt);
                        break;
                    case SpawnPoint.AvatarSpawnType.Red:
                        spwnPt.SpawnPosition = spawn.transform;
                        spwnPt.PlayerID = PlayerLabel.Two;
                        OriginalSpawns.Add(spwnPt);
                        break;
                    case SpawnPoint.AvatarSpawnType.Green:
                        spwnPt.SpawnPosition = spawn.transform;
                        spwnPt.PlayerID = PlayerLabel.Three;
                        OriginalSpawns.Add(spwnPt);
                        break;
                    case SpawnPoint.AvatarSpawnType.Purple:
                        spwnPt.SpawnPosition = spawn.transform;
                        spwnPt.PlayerID = PlayerLabel.Four;
                        OriginalSpawns.Add(spwnPt);
                        break;
                    default:
                        break;
                }
            }

            avatarContainer = new GameObject("AvatarContainer");
            avatarContainer.transform.parent = GameManager.Instance.LevelMng.transform;
        }

        #region API
        /// <summary>
        /// Respawn a Player without cooldown
        /// </summary>
        /// <param name="_player">Player to spawn</param>
        void Spawn(Player _player)
        {
            //con una lista che prevede il corretto criterio di selezione degli spawn points.
            foreach (AvatarSpawnPoint spawn in OriginalSpawns)
            {
                if (spawn.PlayerID == _player.ID)
                {
                    Avatar newAgent = _player.Avatar;
                    newAgent.State = AvatarState.Enabled;
                    newAgent.transform.position = spawn.SpawnPosition.position;
                    newAgent.transform.rotation = spawn.SpawnPosition.rotation;
                    newAgent.transform.parent = avatarContainer.transform;

                    if(EventManager.OnAgentSpawn != null)
                        EventManager.OnAgentSpawn(newAgent.GetComponentInChildren<Avatar>());
                    return;                    
                }
            }
        }

        /// <summary>
        /// Respawn after a fixed amount of time
        /// </summary>
        /// <param name="_playerIndx">Player to spawn</param>
        public void RespawnAvatar(Player _player, float _spawnTime)
        {
            _player.Avatar.State = AvatarState.Ready;
            StartCoroutine(RespawnCooldown(_player,_spawnTime));
        }

        #endregion

        [Serializable]
        public struct AvatarSpawnPoint
        {
            public Transform SpawnPosition;
            public PlayerLabel PlayerID;
        }

        IEnumerator RespawnCooldown(Player _playerID, float _spawnTime)
        {
            yield return new WaitForSeconds(_spawnTime);
            Spawn(_playerID);
        }
    }
}
