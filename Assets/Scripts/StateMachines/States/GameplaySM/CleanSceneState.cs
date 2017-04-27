using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class CleanSceneState : StateBase {

        public override void OnStart()
        {
            Debug.Log("CleanSceneState");

            GameManager.Instance.LevelMng.CleanPins();
            //GameManager.Instance.LevelMng.DestroyAllAvatars();
            // GameManager.Instance.PlayerMng.Players[0].Avatar
            // TODO: eseguire solo destroy della nave e corda e riesegure "setup" nello stato di levelinit.
            
        }

        public override void OnUpdate() {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}
