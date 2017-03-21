using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class LevelInitState : StateBase
    {
        int roundNumber;
        LevelManager levelManager;

        public LevelInitState(int _roundNumber)
        {
            roundNumber = _roundNumber;
            levelManager = GameObject.FindObjectOfType<LevelManager>();
        }

        public override void OnStart()
        {
            Debug.Log("LevelInitState");
            // TODO : sportare questa chiamata in un evento
            levelManager.OnLevelStart();
            EventManager.OnLevelInit();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}