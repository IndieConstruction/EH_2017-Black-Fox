using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using TMPro;
using UnityEngine.UI;

namespace BlackFox
{
    public class EndRoundlUI : MonoBehaviour
    {

        public TMP_Text Player1Points;
        public TMP_Text Player2Points;
        public TMP_Text Player3Points;
        public TMP_Text Player4Points;
        public GameObject EndLevelPanel;
        Button NextStateButton;

        int P1KillPoints;
        int P2KillPoints;
        int P3KillPoints;
        int P4KillPoints;
        

        // Use this for initialization
        void Start()
        {
            EndLevelPanel.SetActive(false);
            NextStateButton = GetComponentInChildren<Button>();
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        /// <summary>
        /// Attaccato Ad un bottone scatena l'evento per avvisare il current state che deve terminare.
        /// </summary>
        public void ChangeStateOnClick()
        {
            OnClickToChangeState();
        }

        void AddKillPointToUI(Agent _attacker, Agent _victim)
        {
            switch (_attacker.playerIndex)
            {
                case PlayerIndex.One:
                    P1KillPoints++;
                    break;
                case PlayerIndex.Two:
                    P2KillPoints++;
                    break;
                case PlayerIndex.Three:
                    P3KillPoints++;
                    break;
                case PlayerIndex.Four:
                    P4KillPoints++;
                    break;
                default:
                    break;
            }

            switch (_victim.playerIndex)
            {
                case PlayerIndex.One:
                    if (P1KillPoints != 0)
                        P1KillPoints--; 
                    break;
                case PlayerIndex.Two:
                    if (P2KillPoints != 0)
                        P2KillPoints--;
                    break;
                case PlayerIndex.Three:
                    if (P3KillPoints != 0)
                        P3KillPoints--;
                    break;
                case PlayerIndex.Four:
                    if (P4KillPoints != 0)
                        P4KillPoints--;
                    break;
                default:
                    break;
            }
            UpdateUIPoints();
        }

        void UpdateUIPoints()
        {
            Player1Points.text = P1KillPoints + " / 5";
            Player2Points.text = P2KillPoints + " / 5";
            Player3Points.text = P3KillPoints + " / 5";
            Player4Points.text = P4KillPoints + " / 5";
        }

        #region API 
        public void ClearTheUIPoints()
        {
            P1KillPoints = 0;
            P2KillPoints = 0;
            P3KillPoints = 0;
            P4KillPoints = 0;
        }

        #endregion

        #region Events

        public delegate void ChangeStateEvent();

        public ChangeStateEvent OnClickToChangeState;

                
        protected void OnEnable()
        {
            EventManager.OnAgentKilled += AddKillPointToUI;
        }

        protected void OnDisable()
        {
            EventManager.OnAgentKilled -= AddKillPointToUI;
        }
        #endregion
    }
}