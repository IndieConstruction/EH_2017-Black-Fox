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
            GameManager.Instance.UiMng.CurrentMenu = GameManager.Instance.UiMng.endRoundUI;
            GameManager.Instance.UiMng.endRoundUI.SetEndRoundPanelStatus(true);
            GameManager.Instance.PlayerMng.ChangeAllPlayersStateExceptOne(PlayerState.MenuInput, PlayerLabel.One, PlayerState.Blocked);
        }
        
        public override void OnEnd()
        {
            GameManager.Instance.UiMng.endRoundUI.SetEndRoundPanelStatus(false);
            GameManager.Instance.LevelMng.ClearPoints();
        }
    }
}
