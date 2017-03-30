using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class LevelInitState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("LevelInitState");
            GameManager.Instance.levelManager.InitSpawnerManager();
            GameManager.Instance.levelManager.InitCore();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}