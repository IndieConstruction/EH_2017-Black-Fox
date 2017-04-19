using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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

        int currentIndexSelection = 0;

        public int CurrentIndexSelection
        {
            get { return 1; }
            set { currentIndexSelection = value; }
        }

        List<ISelectable> selectableButton = new List<ISelectable>();

        public List<ISelectable> SelectableButtons
        {
            get { return selectableButton; }
            set { selectableButton = value; }
        }

        void Start()
        {
            EndLevelPanel.SetActive(false);
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
                switch (_attacker.PlayerId)
                {
                    case PlayerLabel.One:
                        P1KillPoints++;
                        break;
                    case PlayerLabel.Two:
                        P2KillPoints++;
                        break;
                    case PlayerLabel.Three:
                        P3KillPoints++;
                        break;
                    case PlayerLabel.Four:
                        P4KillPoints++;
                        break;
                    default:
                        break;
                } 
            }

            switch (_victim.PlayerId)
            {
                case PlayerLabel.One:
                    if (P1KillPoints != 0)
                        P1KillPoints--;
                    break;
                case PlayerLabel.Two:
                    if (P2KillPoints != 0)
                        P2KillPoints--;
                    break;
                case PlayerLabel.Three:
                    if (P3KillPoints != 0)
                        P3KillPoints--;
                    break;
                case PlayerLabel.Four:
                    if (P4KillPoints != 0)
                        P4KillPoints--;
                    break;
                default:
                    break;
            }

            EventName.text = GameManager.Instance.LevelMng.EndLevelPanelLable;

            UpdateUIPoints();
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