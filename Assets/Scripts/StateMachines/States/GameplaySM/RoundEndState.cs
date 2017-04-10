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
            GameManager.Instance.UiMng.endRoundUI.EndLevelPanel.SetActive(true);
            SetPlayersState();
        }

        
        public override void OnEnd()
        {
            GameManager.Instance.UiMng.endRoundUI.EndLevelPanel.SetActive(false);
            GameManager.Instance.UiMng.endRoundUI.ClearTheUIPoints();
        }

        void SetPlayersState()
        {
            GameManager.Instance.PlayerMng.ChangePlayerState(PlayerState.MenuInputState, PlayerIndex.One);
            GameManager.Instance.PlayerMng.ChangePlayerState(PlayerState.Blocked, PlayerIndex.Two);
            GameManager.Instance.PlayerMng.ChangePlayerState(PlayerState.Blocked, PlayerIndex.Three);
            GameManager.Instance.PlayerMng.ChangePlayerState(PlayerState.Blocked, PlayerIndex.Four);
        }
    }
}
