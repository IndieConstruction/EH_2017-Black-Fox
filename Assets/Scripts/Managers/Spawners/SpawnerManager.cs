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
        public int Level;
        public int Round;
        List<SpawnerBase> Spawners = new List<SpawnerBase>();
        //TODO: find a better way to organize SpawnerReferences
        private void Awake()
        {
            foreach (var spawner in GetComponentsInChildren<SpawnerBase>())
                Spawners.Add(spawner);
        }

        private void OnEnable()
        {   
            //Initialize each Spawner and subscribe their events
            foreach (var spawner in Spawners)
            {
                spawner.Init(this, Level, Round);
                spawner.OnFlowEnd += HandleOnFlowEnd;
            }
        }

        private void OnDisable()
        {
            //Break all running Spawner
            foreach (var spawner in Spawners)
            {
                spawner.enabled = false;
            }
        }

        #region API
        public void ReInitLevel()
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
        #endregion

        /// <summary>
        /// Manage the OnFlowEnd evennt, removing the Spawner from the list of the current active
        /// and unsubscribing this from that Spawner event OnFlowEnd
        /// </summary>
        /// <param name="_spawner">The Spawner who triggered the event</param>
        void HandleOnFlowEnd(SpawnerBase _spawner)
        {
            Spawners.Remove(_spawner);
            _spawner.OnFlowEnd -= HandleOnFlowEnd;
        }
    }
}
