using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PreInitState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PreInitState");
            GameManager.Instance.LevelMng.InstantiateArena();
            GameManager.Instance.LevelMng.InstantiateAvatarSpawner();
            GameManager.Instance.LevelMng.InstantiateRopeManager();
            GameManager.Instance.LevelMng.InstantiateSpawnerManager();
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.Blocked);
            GameManager.Instance.PlayerMng.SetupAvatars(true);
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}