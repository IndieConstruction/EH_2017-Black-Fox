using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public static class EventManager
    {
        #region GamePlayEvent
        public delegate void GamePlayEvent();

        public static GamePlayEvent OnLevelInit;
        public static GamePlayEvent OnRoundPlay;
        public static GamePlayEvent OnRoundEnd;
        public static GamePlayEvent OnPreStart;
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