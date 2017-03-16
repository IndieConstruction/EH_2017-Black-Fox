using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class PlayState : StateBase
    {
        bool playerWinning;
        int roundNumber;

        public PlayState(int _roundNumber)
        {
            roundNumber = _roundNumber;
        }

        public override void OnStart()
        {
            Debug.Log("RoundState");
            LevelManager.OnPlayerWinnig += HandleOnPlayerWinnig;
        }

        public override void OnUpdate()
        {
            if (playerWinning)
            {
                Debug.Log("Agent Wins");
                if (OnStateEnd != null)
                    OnStateEnd();
            }
        }

        void RoundEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            LevelManager.OnPlayerWinnig -= HandleOnPlayerWinnig;
        }

        #region Events
        void HandleOnPlayerWinnig()
        {
            playerWinning = true;
        }
        #endregion
    }
}
