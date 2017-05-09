using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class EndRoundlUI : MonoBehaviour, IMenu
    {
        public Text[] PlayerPoints = new Text[4];
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
        void ShowAvatarsKillPoints()
        {
            for (int i = 0; i < PlayerPoints.Length; i++)
            {
                PlayerPoints[i].text = GameManager.Instance.LevelMng.GetPlayerKillPoints((PlayerLabel)i+1) + " / " + GameManager.Instance.LevelMng.PointsToWin;
            }
        }
        
        #region API
        /// <summary>
        /// Attiva End Round Panel e mostrare i punti degli avatar
        /// </summary>
        public void SetEndRoundPanelStatus(bool _status)
        {
            if (_status)
            {
                ShowAvatarsKillPoints();
                EventName.text = GameManager.Instance.LevelMng.EndLevelPanelLableText;
            }
            EndLevelPanel.SetActive(_status);
        }

        public void Selection()
        {
            GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
        }
        #endregion
    }
}