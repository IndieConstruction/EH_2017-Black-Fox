using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PreInitState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PreInitState");
            LoadArena();
            LoadManager();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        /// <summary>
        /// Chiama la funzione del level manager che istanzia i manager degli elementi principali del livello
        /// </summary>
        void LoadManager()
        {
            GameManager.Instance.levelManager.InstantiateSpawnerManager();
            GameManager.Instance.levelManager.InstantiateRopeManager();
        }

        /// <summary>
        /// Chiama la funzione del level manager che istanzia l'arena
        /// </summary>
        void LoadArena()
        {
            GameManager.Instance.levelManager.InstantiateLevel();
        }
    }
}