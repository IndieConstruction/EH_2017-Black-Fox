using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class AvatarSelectionState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("AvatarSelectionState");
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}