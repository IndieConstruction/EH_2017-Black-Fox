﻿using System.Collections;
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

            if(Camera.main.GetComponent<AudioListener>() != null)
                Camera.main.GetComponent<AudioListener>().enabled = true;

            GameManager.Instance.PlayerMng.ChangeAllPlayersStateExceptOne(PlayerState.MenuInput, PlayerLabel.One, PlayerState.Blocked);
            GameManager.Instance.LoadingCtrl.DeactivateLoadingPanel();
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.MenuInput);
        }

        public override void OnEnd()
        {
            GameManager.Instance.UiMng.DestroyMainMenu();
        }
    }
}
