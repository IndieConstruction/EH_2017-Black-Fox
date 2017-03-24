﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class GameplaySM : StateMachineBase
    {
        int roundNumber;
        int lelvelNumber;
        // TODO : creare set up parametri
        public int MaxRound;

        private void Start()
        {
            CurrentState = new LevelInitState();
        }

        protected override void OnCurrentStateEnded()
        {
            if ("BlackFox.LevelInitState" == CurrentState.StateName)
            {
                // LevelInitState
                StartCoroutine(RoundInitCountDown(3));
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
                    CurrentState = new GameOverState(lelvelNumber);
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

        public void SetRoundAndLevelNumber(int _levelNumber, int _roundNumber)
        {
            roundNumber = _roundNumber;
            lelvelNumber = _levelNumber;
        }

        IEnumerator RoundInitCountDown(int _time)
        {
            float StartTime = 0f;
            while(StartTime + Time.deltaTime < _time)
            {
                yield return new WaitForEndOfFrame();
            }
            CurrentState = new PlayState();
        }
    }
}
