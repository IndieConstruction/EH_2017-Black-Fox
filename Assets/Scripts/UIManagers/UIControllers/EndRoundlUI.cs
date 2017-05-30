using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class EndRoundlUI : BaseMenu
    {
        public Sprite VictoryImg;
        public Sprite DefeatImg;
        public Image RecapImage;

        public Text[] PlayerPoints = new Text[4];
        public GameObject EndLevelPanel;

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
                PlayerPoints[i].text = GameManager.Instance.LevelMng.GetPlayerKillPoints((PlayerLabel)i+1) + " / " + GameManager.Instance.LevelMng.levelOptions.PointsToWin;
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
            }
            EndLevelPanel.SetActive(_status);
        }

        public override void Selection(Player _player)
        {
            GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Selection);
        }

        /// <summary>
        /// Cambia l'immagine del drappo da visualizzare
        /// </summary>
        /// <param name="_string">"Victory" per impostare il drappo di vittoria. "Defeat" per impostare il drappo di sconfitta</param>
        public void SetRecapImage(string _string)
        {
            switch (_string)
            {
                case "Victory":
                    RecapImage.sprite = VictoryImg;
                    break;
                case "Defeat":
                    RecapImage.sprite = DefeatImg;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }
}