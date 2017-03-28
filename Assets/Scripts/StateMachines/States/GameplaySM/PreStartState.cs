using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PreStartState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PreStartState");
            // TODO : togliere if e capire perchè la chiamata all'evento è nulla
            if(EventManager.OnPreStart != null)
                EventManager.OnPreStart();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}