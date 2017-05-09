using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class UpgradeMenuState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("UpgradeMenuState");
            GameManager.Instance.UiMng.CurrentMenu = GameManager.Instance.UiMng.canvasGameMenu.upgradeMenuManager;
            GameManager.Instance.UiMng.canvasGameMenu.upgradeMenuManager.UpgradePanel.SetActive(true);
            GameManager.Instance.UiMng.canvasGameMenu.upgradeMenuManager.OnStart();
        }

        public override void OnEnd()
        {
            GameManager.Instance.UiMng.canvasGameMenu.upgradeMenuManager.UpgradePanel.SetActive(false);
        }
    }
}