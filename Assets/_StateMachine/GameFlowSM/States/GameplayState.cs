using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayState : StateBase {

    public override void OnStart() {
        SceneManager.LoadScene("PrototypeScene");
    }

}
