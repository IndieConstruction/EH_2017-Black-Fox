using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class LevelSelectionState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("LevelSelectionState");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnStateEnd != null)
                    OnStateEnd();
            }
        }
    }
}