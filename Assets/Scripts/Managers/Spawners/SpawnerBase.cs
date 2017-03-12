using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public abstract class SpawnerBase
    {
        /// <summary>
        /// Runned as first
        /// </summary>
        public virtual void OnActivation()
        {

        }

        /// <summary>
        /// Runned on Unity Update
        /// </summary>
        public virtual void OnRuntime()
        {

        }

        /// <summary>
        /// Runned as last
        /// </summary>
        public virtual void OnDeactivation()
        {

        }

        /// <summary>
        /// Runned as first
        /// </summary>
        public void OnFlowActivation()
        {
            OnActivation();
        }

        /// <summary>
        /// Runned on Unity Update
        /// </summary>
        public void OnFlowRuntime()
        {
            OnRuntime();
        }

        /// <summary>
        /// Runned as last
        /// </summary>
        public void OnFlowDeactivation()
        {
            OnDeactivation();
            if (OnFlowEnd != null)
                OnFlowEnd(this);
        }

        #region Events
        public delegate void SpawnerEvent(SpawnerBase _spawner);

        public event SpawnerEvent OnFlowProgression;
        public event SpawnerEvent OnFlowEnd;
        #endregion
    }
}
