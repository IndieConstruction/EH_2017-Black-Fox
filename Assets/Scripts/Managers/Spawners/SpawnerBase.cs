using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    [Serializable]
    public abstract class SpawnerBase : MonoBehaviour
    {
        public SpawnerOptions Options;

        protected int level;
        protected int round;

        #region API
        /// <summary>
        /// Method needed to initialize the Spawner
        /// </summary>
        /// <param name="_sManager"></param>
        public virtual void Init(int _level, int _round)
        {
            level = _level;
            round = _round;
        }
        /// <summary>
        /// Initialize the option of the Spawner
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual SpawnerBase Init(SpawnerOptions options) {
            Options = options;
            return this;
        }
        /// <summary>
        /// Activate/Deactivate the Spawner
        /// </summary>
        public virtual void Toggle() { }
        /// <summary>
        /// Run it as beginning of round
        /// </summary>
        public virtual void Restart() { }
        /// <summary>
        /// Destroy all the Spawned gameobject from scene
        /// </summary>
        public virtual void CleanSpawned() { }
        #endregion
    }
    
    public class SpawnerOptions {
        public GameObject SpawnerPrefab;

        public void CreateInstance(SpawnerOptions _option, Transform _container) {
            SpawnerBase spawner = null;
            if (_option.SpawnerPrefab != null) { 
                spawner = GameObject.Instantiate<GameObject>(SpawnerPrefab, _container).GetComponent<SpawnerBase>();
                spawner.Init(this);
            }
        }
    }
}
