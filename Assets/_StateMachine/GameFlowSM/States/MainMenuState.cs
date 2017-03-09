using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : StateBase {

    public override void OnStart() {
        //SceneManager.LoadScene("MainMenu");

    }

    public override void OnUpdate() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            stateMachine.CurrentState = new GameplayState();
        }
        if (Input.GetKeyDown(KeyCode.M)) {
            stateMachine.CurrentState = new MainMenuState();
        }
    }
}
