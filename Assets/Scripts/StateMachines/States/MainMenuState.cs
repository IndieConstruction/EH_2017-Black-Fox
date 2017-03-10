using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class MainMenuState : StateBase
    {

        public override void OnStart()
        {
            Debug.Log("MainMenu");
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
