using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackFox
{
    public class MainMenuState : StateBase
    {

        public override void OnStart()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnStateEnd != null)
                    OnStateEnd("MainMenuState");
            }

        }
    }
}
