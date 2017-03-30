using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public static class EventManager
    {
        #region Event For StateMachine

        public delegate void ChangeStateEvent();

        public static ChangeStateEvent OnClickToChangeState;

        #endregion

        #region AgentSpawn
        public delegate void AgentSpawnEvent(Agent _agent);

        public static AgentSpawnEvent OnAgentSpawn;
        #endregion

        #region AgentKilledEvent

        public delegate void AgentKilledEvent(Agent _killer, Agent _victim);

        public static AgentKilledEvent OnAgentKilled;

        #endregion

        #region LevelEvent

        public delegate void LevelEvent();

        public static LevelEvent OnPlayerWinnig;
        public static LevelEvent OnCoreDeath;
        public static LevelEvent OnPointsUpdate;
        
        #endregion
    }
}