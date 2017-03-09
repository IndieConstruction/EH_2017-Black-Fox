using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Reppresenta lo stato di gameplay della flow state machine.
/// </summary>
public class GameplayState : StateBase {

    public override void OnStart() {
        SceneManager.LoadScene("PrototypeScene");
    }

}
