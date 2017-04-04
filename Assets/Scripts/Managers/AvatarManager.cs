using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class AvatarManager : MonoBehaviour
    {

        AgentSM agentSM;

        void Start()
        {
            StartAgentSM();
        }

        #region API
        /// <summary>
        /// Cambia lo stato della AgentSM a seconda dell'enumerativo passato
        /// </summary>
        /// <param name="_nextState"></param>
        public void ChangeAgentSMState(AgentSMStates _nextState)
        {
            if(agentSM != null)
                agentSM.GoToState(_nextState);
        }
        #endregion

        #region AgentSM
        /// <summary>
        /// Istaniuzia la AgentSM
        /// </summary>
        void StartAgentSM()
        {
            agentSM = gameObject.AddComponent<AgentSM>();
        }
        #endregion
    }
}

