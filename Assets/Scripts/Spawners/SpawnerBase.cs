using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    [Serializable]
    public abstract class SpawnerBase : MonoBehaviour
    {
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

        public virtual void ReactToOnAgentKilled(Agent _killer, Agent _victim) { }
        #endregion
    }
}
