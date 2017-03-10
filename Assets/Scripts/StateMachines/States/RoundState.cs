using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class RoundState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("RoundState");
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (OnStateEnd != null)
                    OnStateEnd("RoundState");
            }

        }
    }
}
