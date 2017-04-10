using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

using UnityEngine.UI;
using System;

namespace BlackFox
{
    public class EndRoundlUI : MonoBehaviour, IMenu
    {

        public Text Player1Points;
        public Text Player2Points;
        public Text Player3Points;
        public Text Player4Points;
        public Text EventName;
        public GameObject EndLevelPanel;

        int P1KillPoints;
        int P2KillPoints;
        int P3KillPoints;
        int P4KillPoints;

        int totalIndexSelection = 1;
        int currentInexSelection = 1;

        public int CurrentInexSelection
        {
            get { return 1; }
            set { currentInexSelection = value; }
        }

        public int TotalIndexSelection {
            get { return 1; }
            set { totalIndexSelection = value; }
            }


        // Use this for initialization
        void Start()
        {
            EndLevelPanel.SetActive(false);
            GameManager.Instance.UiMng.CurrentMenu = this;
        }


        void UpdateUIPoints()
        {
            Player1Points.text = P1KillPoints + " / 5";
            Player2Points.text = P2KillPoints + " / 5";
            Player3Points.text = P3KillPoints + " / 5";
            Player4Points.text = P4KillPoints + " / 5";
        }

        #region API 

        public void AddKillPointToUI(Avatar _attacker, Avatar _victim)
        {

            if (_attacker != null)
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
            EventName.text = GameManager.Instance.LevelMng.EndLevelPanelLable;
        }

        public void ClearTheUIPoints()
        {
            P1KillPoints = 0;
            P2KillPoints = 0;
            P3KillPoints = 0;
            P4KillPoints = 0;
        }

        public void Selection()
        {
            GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
        }

        #endregion


    }
}