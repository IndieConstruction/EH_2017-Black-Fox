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
            GameManager.Instance.uiManager.endRoundUI.EndLevelPanel.SetActive(true);
        }

        public override void OnUpdate() {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit")) {
                if (OnStateEnd != null)
                    OnStateEnd();
            }
        }

        public override void OnEnd()
        {
            GameManager.Instance.uiManager.endRoundUI.EndLevelPanel.SetActive(false);
            GameManager.Instance.uiManager.endRoundUI.ClearTheUIPoints();
        }
    }
}
