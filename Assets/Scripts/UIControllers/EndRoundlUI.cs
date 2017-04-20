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
            // TODO : leggere i punti dal level manager e settarli dalla macchina a stati
            //Player1Points.text = P1KillPoints + " / 5";
            //Player2Points.text = P2KillPoints + " / 5";
            //Player3Points.text = P3KillPoints + " / 5";
            //Player4Points.text = P4KillPoints + " / 5";
        }

        #region API 

        public void AddKillPointToUI(Avatar _attacker, Avatar _victim)
        {

            EventName.text = GameManager.Instance.LevelMng.EndLevelPanelLable;

            UpdateUIPoints();
        }

        public void Selection()
        {
            GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
        }

        #endregion
    }
}