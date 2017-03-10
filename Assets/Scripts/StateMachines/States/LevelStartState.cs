using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class LevelStartState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("LevelStart");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnStateEnd != null)
                    OnStateEnd("LevelStartState");
            }

        }
    }
}
