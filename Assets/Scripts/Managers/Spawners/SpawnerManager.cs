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
        [SerializeField]
        public List<SpawnerBase> Spawners = new List<SpawnerBase>();

        private void OnEnable()
        {   
            //Initialize each Spawner and subscribe their events
            foreach (var spawner in Spawners)
            {
                spawner.Init(this);
                spawner.OnFlowEnd += HandleOnFlowEnd;
            }
        }

        void OnDestroy()
        {
            //Break all running Spawner
            foreach (var spawner in Spawners)
            {
                spawner.OnFlowEnd -= HandleOnFlowEnd;
            }
        }

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
