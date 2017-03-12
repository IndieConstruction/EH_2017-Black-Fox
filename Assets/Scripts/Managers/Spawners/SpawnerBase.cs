using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public abstract class SpawnerBase : MonoBehaviour
    {
        protected SpawnerManager sManager;

        #region Virtual Methods
        /// <summary>
        /// Method needed to initialize the Spawner
        /// </summary>
        /// <param name="_sManager"></param>
        public virtual void Init(SpawnerManager _sManager)
        {
            sManager = _sManager;
        }

        /// <summary>
        /// Runned as first
        /// </summary>
        protected virtual void OnActivation()
        {

        }

        /// <summary>
        /// Runned on Unity Update
        /// </summary>
        protected virtual void OnRuntime()
        {

        }

        /// <summary>
        /// Runned as last
        /// </summary>
        protected virtual void OnDeactivation()
        {

        }
        #endregion

        #region OnFlow
        /// <summary>
        /// Runned as first
        /// </summary>
        private void Start()
        {
            if(sManager == null)
            {
                Debug.Log(gameObject.name + "'s Spawner not initialized!");
                return;
            }
            OnActivation();
        }

        /// <summary>
        /// Runned on Unity Update
        /// </summary>
        private void Update()
        {
            OnRuntime();
        }

        /// <summary>
        /// Runned as last
        /// </summary>
        private void OnDestroy()
        {
            OnDeactivation();
            if (OnFlowEnd != null)
                OnFlowEnd(this);
        }
        private void OnDisable()
        {
            OnDeactivation();
            if (OnFlowEnd != null)
                OnFlowEnd(this);
        }
        #endregion

        #region Events
        public delegate void SpawnerEvent(SpawnerBase _spawner);

        public event SpawnerEvent OnFlowEnd;
        #endregion
    }
}
