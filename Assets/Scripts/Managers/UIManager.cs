using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class UIManager : MonoBehaviour
    {
        [HideInInspector]
        public EndRoundlUI endRoundUI;
        [HideInInspector]
        public GameUIController gameUIController;

        // Use this for initialization
        void Start()
        {
            endRoundUI = GetComponentInChildren<EndRoundlUI>();
            gameUIController = GetComponentInChildren<GameUIController>();
        }

        #region Events

        
        protected void OnEnable()
        {
            EventManager.OnAgentKilled += endRoundUI.AddKillPointToUI;
        }

        protected void OnDisable()
        {
            EventManager.OnAgentKilled -= endRoundUI.AddKillPointToUI;
        }
        #endregion
    }
}