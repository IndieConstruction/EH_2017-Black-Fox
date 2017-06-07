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
            GameManager.Instance.PlayerMng.ChangeAllPlayersStateExceptOne(PlayerState.MenuInput, PlayerLabel.One, PlayerState.Blocked);
        }

        public override void OnEnd()
        {
            
            GameManager.Instance.UiMng.DestroyMainMenu();
        }
    }
}
