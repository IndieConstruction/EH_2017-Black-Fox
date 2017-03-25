using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlayState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PlayState");
            EventManager.OnPlayerWinnig += HandleOnPlayerWinnig;
            EventManager.OnCoreDeath += HandleOnCoreDeath;
            EventManager.OnRoundPlay();
        }

        public override void OnEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            EventManager.OnPlayerWinnig -= HandleOnPlayerWinnig;
            EventManager.OnCoreDeath -= HandleOnCoreDeath;
        }

        #region Events Handler
        void HandleOnPlayerWinnig()
        {
            Debug.Log("Agent Wins");
            OnStateEnd();
        }

        void HandleOnCoreDeath()
        {
            Debug.Log("Core Dead");
            OnStateEnd();
        }
        #endregion
    }
}
