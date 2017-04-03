using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackFox {
    /// <summary>
    /// State machine che gestisce il flow generale dell'applicazione.
    /// </summary>
    public class FlowSM : StateMachineBase {

        FlowSMStates NextState;
        
        private void Start()
        {
            CurrentState = new LoadGameState();
            NextState = FlowSMStates.MainMenuState;
        }

        protected override void OnCurrentStateEnded()
        {
            switch (NextState)
            {
                case FlowSMStates.MainMenuState:
                    CurrentState = new MainMenuState();
                    NextState = FlowSMStates.LevelSelectionState;
                    break;
                case FlowSMStates.LevelSelectionState:
                    CurrentState = new LevelSelectionState();
                    NextState = FlowSMStates.GameplayState;
                    break;
                case FlowSMStates.GameplayState:
                    CurrentState = new GameplayState();
                    NextState = FlowSMStates.MainMenuState;
                    break;
            }            
        }

        #region API
        public void GoToState(FlowSMStates _nextState)
        {
            NextState = _nextState;
        }
        #endregion
    }

    public enum FlowSMStates
    {
        LoadGameState,
        MainMenuState,
        LevelSelectionState,
        GameplayState
    }
}
