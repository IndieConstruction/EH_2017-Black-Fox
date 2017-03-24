using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class MainMenuState : StateBase
    {
        Object canvasMenu;

        public override void OnStart()
        {
            canvasMenu = GameObject.Instantiate(Resources.Load("Prefabs/UI/CanvasMenu"));
            Debug.Log("MainMenuState");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
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
