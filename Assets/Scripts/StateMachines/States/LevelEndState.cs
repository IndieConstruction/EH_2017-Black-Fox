using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class LevelEndState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("LevelEnd");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (OnStateEnd != null)
                    OnStateEnd("LevelEnd");
            }

        }
    }
}
