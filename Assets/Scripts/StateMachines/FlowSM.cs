using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackFox {
    /// <summary>
    /// State machine che gestisce il flow generale dell'applicazione.
    /// </summary>
    public class FlowSM : StateMachineBase {

        private void Start()
        {
            CurrentState = new MainMenuState();
        }

        void HandleOnStateEnd(string _stateName)
        {
            switch (_stateName) {
                case "MainMenuState":
                    CurrentState = new GameplayState();
                    break;
                case "GameplayState":
                    CurrentState = new MainMenuState();
                    break;
                default:
                    break;
            }
        }

        #region Events
        private void OnEnable()
        {
            StateBase.OnStateEnd += HandleOnStateEnd;
        }
        private void OnDisable()
        {
            StateBase.OnStateEnd -= HandleOnStateEnd;
        }
        #endregion
    }
}
