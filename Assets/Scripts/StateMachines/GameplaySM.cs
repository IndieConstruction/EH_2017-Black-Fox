using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class GameplaySM : StateMachineBase
    {
        int roundNumber;
        int lelvelNumber;

        private void Start()
        {
            CurrentState = new LevelInitState(roundNumber);
        }

        protected override void OnCurrentStateEnded()
        {
            if ("BlackFox.LevelInitState" == CurrentState.StateName)
            {
                // LevelInitState
                roundNumber++;
                CurrentState = new PlayState(roundNumber);
            }
            else if ("BlackFox.PlayState" == CurrentState.StateName)
            {
                // PlayState
                CurrentState = new RoundEndState(roundNumber);
            }
            else if ("BlackFox.RoundEndState" == CurrentState.StateName)
            {
                // RoundEndState
                if (roundNumber < 4)
                {
                    roundNumber++;
                    CurrentState = new UpgradeMenuState(roundNumber);
                }
                else
                {
                    CurrentState = new GameOverState(lelvelNumber);
                }
            }
            else if ("BlackFox.UpgradeMenuState" == CurrentState.StateName)
            {
                // UpgradeMenuState
                CurrentState = new LevelInitState(roundNumber);
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
