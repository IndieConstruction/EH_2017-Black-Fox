using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State machine che gestisce il flow generale dell'applicazione.
/// </summary>
public class FlowSM : StateMachineBase {

    private void Start() {
        CurrentState = new MainMenuState();
    }

    public void GoToGamePlay() {
        CurrentState = new GameplayState();
    }

}
