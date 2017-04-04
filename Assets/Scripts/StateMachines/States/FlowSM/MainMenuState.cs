using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class MainMenuState : StateBase
    {

        public override void OnStart()
        {
            Debug.Log("MainMenuState");
            GameManager.Instance.UiMng.CreateMainMenu();
            GameManager.Instance.AvatarMng.ChangeAgentSMState(AgentSMStates.MenuInputState);
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
            GameManager.Instance.UiMng.DestroyMainMenu();
        }
    }
}
