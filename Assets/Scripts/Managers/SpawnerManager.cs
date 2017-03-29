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
        List<SpawnerBase> Spawners = new List<SpawnerBase>();

        private void Start()
        {   
            //Initialize each Spawner and subscribe their events
            foreach (var spawner in Spawners)
            {
                spawner.Init(Level, Round);
            }
        }
        
        void InstantiateSpawners(List<GameObject> _levelSpawners)
        {
            foreach (var spawner in _levelSpawners)
                Spawners.Add(Instantiate(spawner, transform).GetComponent<SpawnerBase>());
        }

        #region API
        public void Init(int _level, int _round, List<GameObject> _levelSpawners)
        {
            Level = _level;
            Round = _round;
            InstantiateSpawners(_levelSpawners);
        }

        /// <summary>
        /// Initialize at each round's restart
        /// </summary>
        public void InitLevel()
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
        public void ReactToOnAgentKilled(Agent _killer, Agent _victim)
        {
            foreach (var spawner in Spawners)
            {
                spawner.ReactToOnAgentKilled(_killer, _victim);
            }
        }
        #endregion
    }
}
