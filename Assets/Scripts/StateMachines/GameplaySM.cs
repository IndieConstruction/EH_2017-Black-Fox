using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class GameplaySM : StateMachineBase
    {
        int roundNumber;

        private void Start()
        {
            CurrentState = new LevelStartState();
        }

        protected override void OnCurrentStateEnded()
        {
            if ("BlackFox.LevelStartState" == CurrentState.StateName)
            {
                // LevelStartState
                roundNumber++;
                CurrentState = new RoundInitState(roundNumber);
            }
            else if ("BlackFox.RoundInitState" == CurrentState.StateName)
            {
                // RoundInitState
                CurrentState = new PlayState(roundNumber);
            }
            else if ("BlackFox.PlayState" == CurrentState.StateName)
            {
                // RoundState
                CurrentState = new RoundEndState();
            }
            else if ("BlackFox.RoundEndState" == CurrentState.StateName)
            {
                // RoundEndState
                if (roundNumber < 4)
                {
                    roundNumber++;
                    CurrentState = new RoundInitState(roundNumber);
                }
                else
                {
                    CurrentState = new GameOverState();
                }
            }
            else if ("BlackFox.GameOverState" == CurrentState.StateName)
            {
                // GameOverState - EXIT POINT
                roundNumber = 0;
                if (OnMachineEnd != null)
                    OnMachineEnd("GameplaySM");
            }
        }
    }
}
