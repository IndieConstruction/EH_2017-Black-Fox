using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class AvatarSelectionState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("AvatarSelectionState");
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.MenuInput);
            GameManager.Instance.UiMng.CreateAvatarSelectionMenu();
            GameManager.Instance.UiMng.CurrentMenu = GameManager.Instance.UiMng.avatarSelectionManager;
            GameManager.Instance.UiMng.avatarSelectionManager.Setup(GameManager.Instance.PlayerMng.Players);
            GameManager.Instance.LoadingCtrl.DeactivateLoadingPanel();
        }

        public override void OnEnd() {
            GameManager.Instance.UiMng.DestroyAvatarSelectionMenu();
        }
    }
}