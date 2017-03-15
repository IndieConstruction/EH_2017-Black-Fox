using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class MainMenuState : StateBase
    {
        UnityEngine.Object canvasMenu;

        public override void OnStart()
        {
            canvasMenu = GameObject.Instantiate(Resources.Load("Prefabs/Misc/CanvasMenu"));
            Debug.Log("MainMenu");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnStateEnd != null)
                    OnStateEnd();
            }
        }

        public override void OnEnd()
        {
            GameObject.Destroy(canvasMenu);
        }
    }
}
