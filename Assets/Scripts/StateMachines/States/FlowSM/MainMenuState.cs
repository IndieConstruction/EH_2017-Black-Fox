using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class MainMenuState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("MainMenuState");
            GameManager.Instance.UiMng.CreateMainMenu();
            SetPlayersState();
        }

        public override void OnEnd()
        {
            GameManager.Instance.UiMng.DestroyMainMenu();
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
