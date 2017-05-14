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
            GameManager.Instance.PlayerMng.InstantiatePlayers();
            GameManager.Instance.InstantiateUIManager();
            GameManager.Instance.InstantiateCoinManager();
            GameManager.Instance.InstantiateAudioManager();
            GameManager.Instance.InstantiateUpgradePointsManager();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}