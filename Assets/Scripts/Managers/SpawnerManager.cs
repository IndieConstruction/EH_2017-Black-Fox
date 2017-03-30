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
            ////Initialize each Spawner and subscribe their events
            //foreach (var spawner in Spawners)
            //{
            //     spawner.Init(Level, Round);
            //}
        }
        
        
        #region API


        public void Init(int _level, int _round, List<SpawnerOptions> _levelSpawners)
        {
            Level = _level;
            Round = _round;
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
        public void ReactToOnAgentKilled(Agent _victim)
        {
            GetComponentInChildren<AvatarSpawner>().ReactToOnAgentKilled(_victim);
        }
        #endregion
    }
}
