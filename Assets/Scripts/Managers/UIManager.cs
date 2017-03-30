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

        public delegate void ChangeStateEvent();

        public ChangeStateEvent OnClickToChangeState;

        
        protected void OnEnable()
        {
            EventManager.OnAgentKilled += CallAddKillPoinToUI;
        }

        protected void OnDisable()
        {
            EventManager.OnAgentKilled -= CallAddKillPoinToUI;
        }

        #endregion

        /// <summary>
        /// Richiama la funzione AddKillPointToUI presente nell'EndRoundUI
        /// </summary>
        /// <param name="_attacker"></param>
        /// <param name="_victim"></param>
        void CallAddKillPoinToUI(Agent _attacker, Agent _victim)
        {
            endRoundUI.AddKillPointToUI(_attacker, _victim);
        }
    }
}