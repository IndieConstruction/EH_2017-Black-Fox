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
        
        /// <summary>
        /// Cerca il totale di ogni player e lo mostra in una casella di testo
        /// </summary>
        void GetAvatasKillPoints()
        {
            for (int i = 1; i <= 4; i++)
            {
                int tempPoints = GameManager.Instance.LevelMng.GetPlayerKillPoints((PlayerLabel)i);
                switch (i)
                {
                    case 1:
                        Player1Points.text = tempPoints + " / 5";
                        break;
                    case 2:
                        Player2Points.text = tempPoints + " / 5";
                        break;
                    case 3:
                        Player3Points.text = tempPoints + " / 5";
                        break;
                    case 4:
                        Player4Points.text = tempPoints + " / 5";
                        break;
                }
            }

        }
        
        #region API

        /// <summary>
        /// Attiva End Round Panel e mostrare i punti degli avatar
        /// </summary>
        public void SetEndRoundPanelStatus(bool _status)
        {
            EndLevelPanel.SetActive(_status);
            if (_status)
                GetAvatasKillPoints(); 
        }

        public void Selection()
        {
            GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
        }

        #endregion

    }
}