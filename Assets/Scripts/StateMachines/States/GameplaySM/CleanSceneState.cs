using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class CleanSceneState : StateBase {

        public override void OnStart()
        {
            Debug.Log("CleanSceneState");

            GameManager.Instance.LevelMng.CleanPins();
            GameManager.Instance.LevelMng.AvatarSpwn.DestroyAgents();
        }

        public override void OnUpdate() {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}
