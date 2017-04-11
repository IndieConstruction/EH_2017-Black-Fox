using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PauseState : StateBase
    {
        // TODO : uso di time scale corretto 
        public override void OnStart()
        {
            Debug.Log("PauseState");
            Time.timeScale = 0;
        }

        public override void OnEnd()
        {
            Time.timeScale = 1;
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.PlayInputState);
        }
    }
}