using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    /// <summary>
    /// State machine che gestisce il flow generale dell'applicazione.
    /// </summary>
    public class FlowSM : StateMachineBase
    {
        private void Start()
        {
            CurrentState = new LoadGameState();
        }

        protected override void OnCurrentStateEnded()
        {
            switch (CurrentState.StateName)
            {
                case "BlackFox.LoadGameState":
                    CurrentState = new MainMenuState();
                    break;
                case "BlackFox.MainMenuState":
                    CurrentState = new LevelSelectionState();
                    break;
                case "BlackFox.LevelSelectionState":
                    CurrentState = new GameplayState();
                    break;
                case "BlackFox.GameplayState":
                    CurrentState = new MainMenuState();
                    break;
            }            
        }

        protected override bool CheckRules(StateBase _newState, StateBase _oldState)
        {
            if (_oldState == null)
                return true;

            switch (_newState.StateName)
            {
                case "BlackFox.LoadGameState":
                    return true;
                case "BlackFox.MainMenuState":
                    if (_oldState.StateName == "BlackFox.LoadGameState" || _oldState.StateName == "BlackFox.LevelSelectionState" || _oldState.StateName == "BlackFox.GameplayState")
                        return true;
                    break;
                case "BlackFox.LevelSelectionState":
                    if (_oldState.StateName == "BlackFox.MainMenuState")
                        return true;
                    break;
                case "BlackFox.GameplayState":
                    if (_oldState.StateName == "BlackFox.LevelSelectionState")
                        return true;
                    break;
            }

            return false;
        }
    }
}
