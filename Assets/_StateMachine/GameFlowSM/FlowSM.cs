using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowSM : StateMachineBase {

    private void Start() {
        CurrentState = new MainMenuState();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CurrentState = new GameplayState();
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            CurrentState = new MainMenuState();
        }
    }

}
