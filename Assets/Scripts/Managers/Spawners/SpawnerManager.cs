using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class SpawnerManager : MonoBehaviour
    {

        public int Level;
        public int Round;
        public List<SpawnerBase> Spawners = new List<SpawnerBase>();

        private void Awake()
        {
            foreach (var spawner in Spawners)
            {
                spawner.OnFlowEnd += HandleOnFlowEnd;
            }
        }
        private void Start()
        {
            foreach (var spawner in Spawners)
            {
                spawner.OnFlowActivation();
            }
        }

        
        void OnDestroy()
        {
            foreach (var spawner in Spawners)
            {
                spawner.OnFlowDeactivation();
            }
        }

        void HandleOnFlowEnd(SpawnerBase _spawner)
        {
            Spawners.Remove(_spawner);
        }
    }
}
