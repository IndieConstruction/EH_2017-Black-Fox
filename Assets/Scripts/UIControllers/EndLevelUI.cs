using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using TMPro;

namespace BlackFox
{
    public class EndLevelUI : MonoBehaviour
    {

        public TMP_Text Player1Points;
        public TMP_Text Player2Points;
        public TMP_Text Player3Points;
        public TMP_Text Player4Points;
        public GameObject EndLevelPanel;

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
        }

        // Update is called once per frame
        void Update()
        {
            
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

        #region Events

        protected void OnEnable()
        {
            EventManager.OnAgentKilled += AddKillPointToUI;
        }

        protected void OnDisable()
        {

        }
        #endregion
    }
}