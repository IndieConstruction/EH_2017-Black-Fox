using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlayState : StateBase
    {
        bool playerWinning;
        bool coreDeath;
        int roundNumber;

        public PlayState(int _roundNumber)
        {
            roundNumber = _roundNumber;
        }

        public override void OnStart()
        {
            Debug.Log("PlayState");
            LevelManager.OnPlayerWinnig += HandleOnPlayerWinnig;
            LevelManager.OnCoreDeath += HandleOnCoreDeath;
        }

        public override void OnUpdate()
        {
            if (playerWinning)
            {
                Debug.Log("Agent Wins");
                if (OnStateEnd != null)
                    OnStateEnd();
            }
            else if(coreDeath)
            {
                Debug.Log("Core Dead");
                if (OnStateEnd != null)
                    OnStateEnd();
            }
        }

        public override void OnEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            LevelManager.OnPlayerWinnig -= HandleOnPlayerWinnig;
            LevelManager.OnCoreDeath -= HandleOnCoreDeath;
        }

        #region Events
        void HandleOnPlayerWinnig()
        {
            playerWinning = true;
        }

        void HandleOnCoreDeath()
        {
            coreDeath = true;
        }
        #endregion
    }
}
