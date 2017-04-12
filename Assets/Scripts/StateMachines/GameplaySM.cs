using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class GameplaySM : StateMachineBase
    {
        int roundNumber;
        int levelNumber;
        int MaxRound;

        GamePlaySMStates NextState;        

        private void Start()
        {
            Debug.Log("Start_GamePlaySM");
            CurrentState = new PreInitState();
            NextState = GamePlaySMStates.PreInitState;
        }

        protected override void OnCurrentStateEnded()
        {
            switch (NextState)
            {
                case GamePlaySMStates.PreInitState:
                    CurrentState = new PreInitState();
                    NextState = GamePlaySMStates.LevelInitState;
                    break;
                case GamePlaySMStates.LevelInitState:
                    CurrentState = new LevelInitState();
                    NextState = GamePlaySMStates.PreStartState;
                    break;
                case GamePlaySMStates.PreStartState:
                    CurrentState = new PreStartState();
                    NextState = GamePlaySMStates.PlayState;
                    break;
                case GamePlaySMStates.PlayState:
                    CurrentState = new PlayState();
                    NextState = GamePlaySMStates.CleanSceneState;
                    break;
                case GamePlaySMStates.PauseState:
                    CurrentState = new PauseState();
                    NextState = GamePlaySMStates.PlayState;
                    break;
                case GamePlaySMStates.CleanSceneState:
                    CurrentState = new CleanSceneState();
                    NextState = GamePlaySMStates.RoundEndState;
                    break;
                case GamePlaySMStates.RoundEndState:
                    CurrentState = new RoundEndState();
                    if(roundNumber < MaxRound)
                        NextState = GamePlaySMStates.UpgradeMenuState;
                    else
                        NextState = GamePlaySMStates.GameOverState;
                    break;
                case GamePlaySMStates.UpgradeMenuState:
                    CurrentState = new UpgradeMenuState();
                    NextState = GamePlaySMStates.LevelInitState;
                    break;
                case GamePlaySMStates.GameOverState:
                    CurrentState = new GameOverState();
                    if (OnMachineEnd != null)
                        OnMachineEnd("GameplaySM");
                    break;
            }
        }

        #region API
        public void GoToState(GamePlaySMStates _nextState)
        {
            NextState = _nextState;
            if (CurrentState.OnStateEnd != null)
                CurrentState.OnStateEnd();
        }

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

    public enum GamePlaySMStates
    {
        PreInitState,
        LevelInitState,
        PreStartState,
        PlayState,
        PauseState,
        CleanSceneState,
        RoundEndState,
        UpgradeMenuState,
        GameOverState
    }
}
