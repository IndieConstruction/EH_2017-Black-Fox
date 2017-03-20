using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    /// <summary>
    /// Costruisce la scena a runtime
    /// </summary>
    public class LevelStartState : StateBase
    {
        int levelNumber;

        public LevelStartState(int _levelNumber)
        {
            levelNumber = _levelNumber;
        }

        public override void OnStart()
        {
            Debug.Log("LevelStartState");
            LoadArena();
        }
        
        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        /// <summary>
        /// Istanzia componenti dell'arena
        /// </summary>
        void LoadArena()
        {
            GameObject.Instantiate(Resources.Load("Prefabs/Levels/Level" + levelNumber));         
        }
    }
}
