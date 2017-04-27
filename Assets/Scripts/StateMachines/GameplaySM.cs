using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class GameplaySM : StateMachineBase
    {
        GamePlaySMStates NextState;

        public void Init() {
            Debug.Log("Start_GamePlaySM");
            CurrentState = new PreInitState();
            NextState = GamePlaySMStates.LevelInitState;
        }

        protected override void OnCurrentStateEnded()
        {
            switch (NextState)
            {
                case GamePlaySMStates.PreInitState:
                    // caso attualmente non utilizzato
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
                    if(GameManager.Instance.LevelMng.roundNumber < GameManager.Instance.LevelMng.MaxRound)
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
