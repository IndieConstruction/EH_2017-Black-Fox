using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;


namespace BlackFox
{
    public class RoundEndState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("RoundEndState");
            GameManager.Instance.UiMng.CurrentMenu = GameManager.Instance.UiMng.endRoundUI;
            GameManager.Instance.UiMng.endRoundUI.EndLevelPanel.SetActive(true);
            GameManager.Instance.PlayerMng.ChangeAllPlayersStateExceptOne(PlayerState.MenuInput, PlayerLabel.One, PlayerState.Blocked);
        }

        
        public override void OnEnd()
        {
            GameManager.Instance.UiMng.endRoundUI.EndLevelPanel.SetActive(false);
        }
    }
}
