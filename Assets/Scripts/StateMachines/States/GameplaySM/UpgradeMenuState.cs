using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class UpgradeMenuState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("UpgradeMenuState");
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}