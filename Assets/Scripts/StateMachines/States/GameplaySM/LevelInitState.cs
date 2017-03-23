using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class LevelInitState : StateBase
    {
        public LevelInitState()
        {
        }

        public override void OnStart()
        {
            Debug.Log("LevelInitState");
            EventManager.OnLevelInit();
            
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}