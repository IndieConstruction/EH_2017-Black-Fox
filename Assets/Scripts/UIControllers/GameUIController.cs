using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;
using TMPro;

namespace BlackFox
{
    public class GameUIController : MonoBehaviour
    {

        public Text Player1BulletCount;
        public Text Player2BulletCount;
        public Text Player3BulletCount;
        public Text Player4BulletCount;

        public TMP_Text Player1KillPoints;
        public TMP_Text Player2KillPoints;
        public TMP_Text Player3KillPoints;
        public TMP_Text Player4KillPoints;

        public Slider ElementZeroSlider;

        public TMP_Text LevelIndicationText;
        LevelManager levelMNG;

        

        private void Start()
        {
            levelMNG = FindObjectOfType<LevelManager>();
        }

        void UpdateLevelInformation()
        {
            LevelIndicationText.text = "Level: " + levelMNG.lelvelNumber + "/" + "Round: " + levelMNG.roundNumber;
        }

        #region API

        /// <summary>
        /// Quando viene richiamata la funzione, vengono visualizzati i proiettili del player che gli viene passato.
        /// </summary>
        /// <param name="_playerIndex"></param>
        /// <param name="_remainigAmmo"></param>
        public void SetBulletsValue(PlayerIndex _playerIndex, int _remainigAmmo)
        {
            switch (_playerIndex)   
            {
                case PlayerIndex.One:
                    Player1BulletCount.text = _remainigAmmo.ToString();
                    break;
                case PlayerIndex.Two:
                    Player2BulletCount.text = _remainigAmmo.ToString();
                    break;
                case PlayerIndex.Three:
                    Player3BulletCount.text = _remainigAmmo.ToString();
                    break;
                case PlayerIndex.Four:
                    Player4BulletCount.text = _remainigAmmo.ToString();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Quando viene richiamata aggiorna i Kill point per il player che ha ucciso qualcuno e per la vittima
        /// </summary>
        /// <param name="_playerIndex">Il giocatore a cui aggiornare i punti uccisione nella Ui</param>
        /// <param name="_playerPoints">I punti da visualizzare nella UI</param>
        public void SetKillPointsUI(PlayerIndex _playerIndex, int _playerPoints)
        {
            switch (_playerIndex)
            {
                case PlayerIndex.One:
                    Player1KillPoints.text = "Kill Points = " + _playerPoints;
                    break;
                case PlayerIndex.Two:
                    Player2KillPoints.text = "Kill Points = " + _playerPoints;
                    break;
                case PlayerIndex.Three:
                    Player3KillPoints.text = "Kill Points = " + _playerPoints;
                    break;
                case PlayerIndex.Four:
                    Player4KillPoints.text = "Kill Points = " + _playerPoints;
                    break;
                default:
                    break;
            }
        }

        public void SetElementZeroSlider(float _life, float _maxLife)
        {
            ElementZeroSlider.value = _life / _maxLife;                  // Da rivedere se il valore della vita cambia
        }

        #endregion

        #region Events

        private void OnEnable()
        {
            EventManager.OnLevelInit += UpdateLevelInformation;
        }

        private void OnDisable()
        {
            EventManager.OnLevelInit -= UpdateLevelInformation;
        }
        #endregion
    }
}
