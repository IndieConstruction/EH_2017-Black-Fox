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
            EventManager.OnAgentKilled += HandleOnAgentKilled;
        }

        public override void OnEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            EventManager.TriggerPlayStateEnd -= HandleTriggerPlayStateEnd;
            EventManager.OnAgentKilled -= HandleOnAgentKilled;
        }

        #region Events Handler
        void HandleTriggerPlayStateEnd()
        {
            OnStateEnd();
        }

        void HandleOnAgentKilled(Avatar _killer, Avatar _victim)
        {
            GameManager.Instance.LevelMng.AgentKilled(_killer, _victim);
        }
        #endregion
    }
}
