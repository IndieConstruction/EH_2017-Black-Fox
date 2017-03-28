using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class GameplaySM : StateMachineBase
    {
        int roundNumber;
        int levelNumber;
        int MaxRound;

        private void Start()
        {
            CurrentState = new PreInitState(levelNumber, roundNumber);
        }

        protected override void OnCurrentStateEnded()
        {
            if ("BlackFox.PreInitState" == CurrentState.StateName)
            {
                // PreInitState
                CurrentState = new LevelInitState();
            }
            else if ("BlackFox.LevelInitState" == CurrentState.StateName)
            {
                // LevelInitState
                CurrentState = new PreStartState();
            }
            else if ("BlackFox.PreStartState" == CurrentState.StateName)
            {
                // LevelInitState
                CurrentState = new PlayState();
            }
            else if ("BlackFox.PlayState" == CurrentState.StateName)
            {
                // PlayState
                CurrentState = new RoundEndState();
            }
            else if ("BlackFox.RoundEndState" == CurrentState.StateName)
            {
                // RoundEndState
                if (roundNumber <= MaxRound)
                {
                    CurrentState = new UpgradeMenuState();
                }
                else
                {
                    CurrentState = new GameOverState(levelNumber);
                }
            }
            else if ("BlackFox.UpgradeMenuState" == CurrentState.StateName)
            {
                // UpgradeMenuState
                CurrentState = new LevelInitState();
            }
            else if ("BlackFox.GameOverState" == CurrentState.StateName)
            {
                // GameOverState - EXIT POINT
                if (OnMachineEnd != null)
                    OnMachineEnd("GameplaySM");
            }
        }

        #region API
        public void SetRoundNumber(int _roundNumber)
        {
            roundNumber = _roundNumber;
        }

        public void SetMaxRoundNumber(int _maxRoundNumber)
        {
            MaxRound = _maxRoundNumber;
        }

        public void SetLevelNumber(int _levelNumber)
        {
            levelNumber = _levelNumber;
        }
        #endregion
    }
}
