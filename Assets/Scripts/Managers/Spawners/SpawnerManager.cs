using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class SpawnerManager : MonoBehaviour
    {

        public int Level;
        public int Round;



        #region

        public delegate void SMLifeFlow();
        /// <summary>
        /// Exectued as first during LifeFlow
        /// </summary>
        public static event SMLifeFlow OnActivation;
        /// <summary>
        /// Keep executing while alive
        /// </summary>
        public static event SMLifeFlow OnRuntime;
        /// <summary>
        /// Executed as last
        /// </summary>
        public static event SMLifeFlow OnDeactivation;
        #endregion
    }
}
