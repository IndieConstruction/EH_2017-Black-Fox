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

        protected override void OnCurrentStateEnded()  
        {
            if ("BlackFox.MainMenuState" == CurrentState.StateName) {
                // MainMenuState
                CurrentState = new LevelSelectionState();
            }
            else if ("BlackFox.LevelSelectionState" == CurrentState.StateName)
            {
                // LevelSelectionState
                CurrentState = new GameplayState();
            }
            else if ("BlackFox.GameplayState" == CurrentState.StateName) {
                // GameplayState
                CurrentState = new MainMenuState();
            }
        }
    }
}
