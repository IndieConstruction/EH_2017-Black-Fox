using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class LevelSelectionState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("LevelSelectionState");
            GameManager.Instance.UiMng.CreateLevelSelectionMenu();
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
            {
                if (OnStateEnd != null)
                    OnStateEnd();
            }
        }

        public override void OnEnd()
        {
            GameManager.Instance.UiMng.DestroyLevelSelectionMenu();
        }
    }
}