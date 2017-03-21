using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class EventManager : MonoBehaviour
    {
        #region GamePlayEvent
        public delegate void GamePlayEvent();

        public static GamePlayEvent OnLevelInit;
        public static GamePlayEvent OnLevelPlay;
        public static GamePlayEvent OnLevelEnd;

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