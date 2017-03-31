﻿using System.Collections;
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
            EventManager.OnAgentSpawn += HandleOnAgentSpawn;
        }

        public override void OnEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            EventManager.TriggerPlayStateEnd -= HandleTriggerPlayStateEnd;
            EventManager.OnAgentKilled -= HandleOnAgentKilled;
            EventManager.OnAgentSpawn -= HandleOnAgentSpawn;
        }

        #region Events Handler
        void HandleTriggerPlayStateEnd()
        {
            OnStateEnd();
        }

        void HandleOnAgentKilled(Agent _killer, Agent _victim)
        {
            GameManager.Instance.levelManager.AgentKilled(_killer, _victim);
        }

        void HandleOnAgentSpawn(Agent _agent)
        {
            GameManager.Instance.levelManager.AgentSpawn(_agent);
        }
        #endregion
    }
}
