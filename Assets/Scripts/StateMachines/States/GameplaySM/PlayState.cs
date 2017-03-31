using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlayState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PlayState");
            EventManager.TriggerPlayStateEnd += HandleTriggerPlayStateEnd;
        }

        public override void OnEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            EventManager.TriggerPlayStateEnd -= HandleTriggerPlayStateEnd;
        }

        #region Events Handler
        void HandleTriggerPlayStateEnd()
        {
            OnStateEnd();
        }
        #endregion
    }
}
