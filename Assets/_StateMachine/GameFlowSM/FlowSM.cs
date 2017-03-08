using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowSM : StateMachineBase {

    private void Start() {
        CurrentState = new MainMenuState();
    }

    public void GoToGamePlay() {
        CurrentState = new GameplayState();
    }

}
