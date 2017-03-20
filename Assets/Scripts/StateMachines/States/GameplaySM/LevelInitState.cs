using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class LevelInitState : StateBase
    {
        int roundNumber;

        public LevelInitState(int _roundNumber)
        {
            roundNumber = _roundNumber;
        }

        public override void OnStart()
        {
            Debug.Log("LevelInitState");
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}