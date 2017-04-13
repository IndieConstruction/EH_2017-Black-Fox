using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PauseState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PauseState");
            Time.timeScale = 0;
            GameManager.Instance.UiMng.CurrentMenu = GameManager.Instance.UiMng.pauseMenuController;
            GameManager.Instance.UiMng.pauseMenuController.ChildrenPanel.SetActive(true);
        }

        public override void OnEnd()
        {
            Time.timeScale = 1;
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.PlayInputState);
            GameManager.Instance.UiMng.pauseMenuController.ChildrenPanel.SetActive(false);
        }
    }
}