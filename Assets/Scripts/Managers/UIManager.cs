using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class UIManager : MonoBehaviour
    {

        EndRoundlUI endRoundUI;
        GameUIController gameUIController;

        // Use this for initialization
        void Start()
        {
            endRoundUI = GetComponentInChildren<EndRoundlUI>();
            gameUIController = GetComponentInChildren<GameUIController>();
        }

        // Update is called once per frame
        void Update()
        {

        }


        #region Events

        public delegate void ChangeStateEvent();

        public ChangeStateEvent OnClickToChangeState;


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