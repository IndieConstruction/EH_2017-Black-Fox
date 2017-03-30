using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class CleanSceneState : StateBase {

        public override void OnStart() {
            Debug.Log("CleanSceneState");

            GameManager.Instance.levelManager.CleanPins();
            GameManager.Instance.levelManager.SpawnerMng.CallDestroyAgents();
        }

        public override void OnUpdate() {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}
