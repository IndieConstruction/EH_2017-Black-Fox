using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class LoadGameState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("LoadGameState");
            GameManager.Instance.InstantiatePlayerManager();
            GameManager.Instance.InstantiateUIManager();
            GameManager.Instance.InstantiateCoinManager();
            GameManager.Instance.InstantiateAudioManager();
            GameManager.Instance.InstantiateDataManager();
            GameManager.Instance.InstantiateShowRoom();
            GameManager.Instance.InstantiateLoadingController();

            GameManager.Instance.PlayerMng.InstantiatePlayers();
            GameManager.Instance.DataMng.Init();
            GameManager.Instance.SRMng.Init(GameManager.Instance.PlayerMng.Players);
            GameManager.Instance.UiMng.Init();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        public override void OnEnd() {
            GameManager.Instance.LoadingCtrl.DeactivateLoadingPanel();
        }
    }
}