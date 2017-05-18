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
            GameManager.Instance.InstantiateUpgradePointsManager();
            GameManager.Instance.InstantiatePowerUpManager();
            GameManager.Instance.InstantiateDataManager();
            GameManager.Instance.InstantiateShowRoom();

            GameManager.Instance.PlayerMng.InstantiatePlayers();
            GameManager.Instance.DataMng.Init();
            GameManager.Instance.SRMng.Init(GameManager.Instance.PlayerMng.Players);
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}