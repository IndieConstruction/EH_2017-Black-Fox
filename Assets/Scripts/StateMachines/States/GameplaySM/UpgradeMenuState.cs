﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class UpgradeMenuState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("UpgradeMenuState");
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.MenuInput);
            GameManager.Instance.UiMng.CurrentMenu = GameManager.Instance.UiMng.canvasGameMenu.upgradeMenuManager;
            GameManager.Instance.UiMng.canvasGameMenu.upgradeMenuManager.UpgradePanel.SetActive(true);
            GameManager.Instance.UiMng.canvasGameMenu.upgradeMenuManager.Init(GameManager.Instance.PlayerMng.Players);
        }

        public override void OnEnd()
        {
            GameManager.Instance.UiMng.canvasGameMenu.upgradeMenuManager.UpgradePanel.SetActive(false);
        }
    }
}