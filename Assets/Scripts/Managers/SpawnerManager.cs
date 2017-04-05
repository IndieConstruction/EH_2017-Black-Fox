using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    /// <summary>
    /// It manages a list of Spawners during their LifeFlow
    /// </summary>
    public class SpawnerManager : MonoBehaviour
    {
        int Level;
        int Round;
        public List<SpawnerBase> Spawners = new List<SpawnerBase>();
        
        #region API
        public void InstantiateNewSpawners(Level _currentLevel)
        {
            Spawners.Add(_currentLevel.ArrowsSpawner.CreateInstance(_currentLevel.ArrowsSpawner, transform));
            Spawners.Add( _currentLevel.BlackHoleSpawner.CreateInstance(_currentLevel.BlackHoleSpawner, transform));
            Spawners.Add( _currentLevel.ExternalElementSpawner.CreateInstance(_currentLevel.ExternalElementSpawner, transform));
            Spawners.Add(_currentLevel.AvatarSpawner.CreateInstance(_currentLevel.AvatarSpawner, transform));
            Spawners.Add( _currentLevel.TurretSpawner.CreateInstance(_currentLevel.TurretSpawner, transform));
            Spawners.Add(_currentLevel.WaveSpawner.CreateInstance(_currentLevel.WaveSpawner, transform));
        }

        public void Init(int _level, int _round, List<SpawnerOptions> _levelSpawners)
        {
            Level = _level;
            Round = _round;
        }

        /// <summary>
        /// Initialize at each round's restart
        /// </summary>
        public void SpawnAgent()
        {
            foreach (var spawner in GetComponentsInChildren<SpawnerBase>())
            {
                if(spawner.GetType() == typeof(AvatarSpawner))
                {
                    AvatarSpawner spawnAvatar = spawner as AvatarSpawner;
                    spawnAvatar.RespawnAllImmediate();
                }
            }
        }
        /// <summary>
        /// Respawn Agent on Death
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        public void ReactToOnAgentKilled(Avatar _victim)
        {
            GetComponentInChildren<AvatarSpawner>().RespawnAvatar(_victim);
        }

        public void CallDestroyAgents(){
            GetComponentInChildren<AvatarSpawner>().DestroyAgents();
        }
        #endregion
    }
}
