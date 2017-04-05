using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlayerSM : StateMachineBase
    {
        AgentSMStates NextState;

        private void Start()
        {
            Debug.Log("Start_PlayerSM");
            CurrentState = new MenuInputState();
        }

        protected override void OnCurrentStateEnded()
        {
            switch (NextState)
            {
                case AgentSMStates.MenuInputState:
                    CurrentState = new MenuInputState();
                    break;
                case AgentSMStates.PlayInputState:
                    CurrentState = new PlayInputState();
                    break;
            }
        }

        #region API
        public void GoToState(AgentSMStates _nextState)
        {
            NextState = _nextState;
            if (CurrentState.OnStateEnd != null)
                CurrentState.OnStateEnd();
        }
        #endregion
    }

    public enum AgentSMStates
    {
        MenuInputState,
        PlayInputState
    }
}
