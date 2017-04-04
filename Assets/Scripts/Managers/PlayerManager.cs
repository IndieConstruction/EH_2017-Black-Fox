using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class PlayerManager : MonoBehaviour
    {
        public GameObject PlayerGenericPrefab;
        [HideInInspector]
        public List<Player> Players = new List<Player>();

        PlayerSM agentSM;

        void Start()
        {
            StartAgentSM();
        }
        
        #region API
        /// <summary>
        /// Instanzia i player
        /// </summary>
        public void InstantiatePlayers()
        {
            for(int i = 0; i < 4; i++)
            {
                Player player = Instantiate(PlayerGenericPrefab, transform).GetComponent<Player>();
                player.playerIndex = (PlayerIndex)i;
                player.name = player.Name = "Player" + player.playerIndex;
                Players.Add(player);
            }
        }

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
            agentSM = gameObject.AddComponent<PlayerSM>();
        }
        #endregion
    }
}

