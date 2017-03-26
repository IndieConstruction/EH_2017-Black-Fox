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

        int P1Kill;
        int P2Kill;
        int P3Kill;
        int P4Kill;

        int P1Dead;
        int P2Dead;
        int P3Dead;
        int P4Dead;

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
                    P1Kill++;
                    break;
                case PlayerIndex.Two:
                    P2Kill++;
                    break;
                case PlayerIndex.Three:
                    P3Kill++;
                    break;
                case PlayerIndex.Four:
                    P4Kill++;
                    break;
                default:
                    break;
            }

            switch (_victim.playerIndex)
            {
                case PlayerIndex.One:
                    P1Dead++;
                    break;
                case PlayerIndex.Two:
                    P2Dead++;
                    break;
                case PlayerIndex.Three:
                    P3Dead++;
                    break;
                case PlayerIndex.Four:
                    P4Dead++;
                    break;
                default:
                    break;
            }
            UpdateUIPoints();
        }

        void UpdateUIPoints()
        {
            Player1Points.text = P1Kill + " / " + P1Dead;
            Player2Points.text = P2Kill + " / " + P2Dead;
            Player3Points.text = P3Kill + " / " + P3Dead;
            Player4Points.text = P4Kill + " / " + P4Dead;
        }

        #region API 
        public void ClearTheUIPoints()
        {
            P1Kill = 0;
            P2Kill = 0;
            P3Kill = 0;
            P4Kill = 0;
            P1Dead = 0;
            P2Dead = 0;
            P3Dead = 0;
            P4Dead = 0;
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