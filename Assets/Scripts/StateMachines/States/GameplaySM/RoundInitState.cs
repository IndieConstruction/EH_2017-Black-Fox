using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class RoundInitState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("RoundInitState");
            //if(!GameManager.Instance.LevelMng.PlayOff)
                GameManager.Instance.LevelMng.SpawnAllAvatar(0);
            //else
            //    GameManager.Instance.LevelMng.SpawnSelectedAvatars(0);

            GameManager.Instance.LevelMng.InitCore();
            GameManager.Instance.LevelMng.SpawnerMng.InitSpawners();
            GameManager.Instance.LevelMng.PowerUpMng.Init();
            GameManager.Instance.UiMng.canvasGame.gameUIController.UpdateLevelInformation();
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.Blocked);
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}