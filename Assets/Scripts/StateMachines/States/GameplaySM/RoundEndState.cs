using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlackFox
{
    public class RoundEndState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("RoundEndState");
            GameManager.Instance.UiMng.endRoundUI.EndLevelPanel.SetActive(true);
        }

        public override void OnUpdate() {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit")) {
                if (OnStateEnd != null)
                    OnStateEnd();
            }
        }

        public override void OnEnd()
        {
            GameManager.Instance.UiMng.endRoundUI.EndLevelPanel.SetActive(false);
            GameManager.Instance.UiMng.endRoundUI.ClearTheUIPoints();
        }
    }
}
