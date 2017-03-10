using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public abstract class SpawnerBase : MonoBehaviour
    {
        protected SpawnerManager spawnerManager;

        SpawnerBase(SpawnerManager _sm) {
            spawnerManager = _sm;
        }


        public virtual void OnActivation()
        {

        }
        public virtual void OnRuntime()
        {

        }
        public virtual void OnDeactivation()
        {

        }

    }
}
