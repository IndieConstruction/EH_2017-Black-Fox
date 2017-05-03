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
            NextState = GamePlaySMStates.RoundInitState;
        }

        protected override void OnCurrentStateEnded()
        {
            switch (CurrentState.StateName) {
                case "BlackFox.PreInitState":
                    CurrentState = new RoundInitState();
                    break;
                case "BlackFox.RoundInitState":
                    CurrentState = new PreStartState();
                    break;
                case "BlackFox.PreStartState":
                    CurrentState = new PlayState();
                    break;
                case "BlackFox.PlayState":
                    CurrentState = new CleanSceneState();
                    break;

                case "BlackFox.PauseState":
                    CurrentState = new PlayState();
                    break;
                case "BlackFox.CleanSceneState":
                    CurrentState = new RoundEndState();
                    break;
                case "BlackFox.RoundEndState":
                    if(GameManager.Instance.LevelMng.roundNumber < GameManager.Instance.LevelMng.MaxRound)
                        CurrentState = new UpgradeMenuState();
                    else
                        CurrentState = new GameOverState();
                    break;
                case "BlackFox.UpgradeMenuState":
                    CurrentState = new RoundInitState();
                    break;
                case "BlackFox.GameOverState":
                    if (GameplaySM.OnMachineEnd != null)
                        GameplaySM.OnMachineEnd("GameplaySM");
                    break;
            }
            
        }

        protected override bool CheckRules(StateBase _newState, StateBase _oldState) 
        {
            if (_oldState == null) 
                return true;
            

            switch (_newState.StateName) {
                case "BlackFox.PreInitState":
                        return true;
                case "BlackFox.RoundInitState":
                    if (_oldState.StateName == "BlackFox.PreInitState")
                        return true;
                    break;
                case "BlackFox.PreStartState":
                    if (_oldState.StateName == "BlackFox.RoundInitState")
                        return true;
                    break;
                case "BlackFox.PlayState":
                    if (_oldState.StateName == "BlackFox.PreStartState")
                        return true;
                    break;
                
                case "BlackFox.PauseState":
                    if (_oldState.StateName == "BlackFox.PlayState")
                        return true;
                    break;
                case "BlackFox.CleanSceneState":
                    if (_oldState.StateName == "BlackFox.PlayState" || _oldState.StateName == "BlackFox.PauseState")
                        return true;
                    break;
                case "BlackFox.RoundEndState":
                    if (_oldState.StateName == "BlackFox.CleanSceneState")
                        return true;
                    break;
                case "BlackFox.UpgradeMenuState":
                    if (_oldState.StateName == "BlackFox.RoundEndState")
                        return true;
                    break;
                case "BlackFox.GameOverState":
                    if (_oldState.StateName == "BlackFox.RoundEndState" || _oldState.StateName == "BlackFox.CleanSceneState")
                        return true;
                    break;

            }
            return false;
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
        RoundInitState,
        PreStartState,
        PlayState,
        PauseState,
        CleanSceneState,
        RoundEndState,
        UpgradeMenuState,
        GameOverState
    }
}
