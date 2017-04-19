using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class GameUIController : MonoBehaviour
    {

        public Text Player1BulletCount;
        public Text Player2BulletCount;
        public Text Player3BulletCount;
        public Text Player4BulletCount;

        public Text Player1KillPoints;
        public Text Player2KillPoints;
        public Text Player3KillPoints;
        public Text Player4KillPoints;

        public Slider ElementZeroSlider;

        public Text LevelIndicationText;       

        

        #region API

        /// <summary>
        /// Quando viene richiamata la funzione, vengono visualizzati i proiettili del player che gli viene passato.
        /// </summary>
        /// <param name="_playerID"></param>
        /// <param name="_remainigAmmo"></param>
        public void SetBulletsValue(PlayerLabel _playerID, int _remainigAmmo)
        {
            switch (_playerID)   
            {
                case PlayerLabel.One:
                    Player1BulletCount.text = _remainigAmmo.ToString();
                    break;
                case PlayerLabel.Two:
                    Player2BulletCount.text = _remainigAmmo.ToString();
                    break;
                case PlayerLabel.Three:
                    Player3BulletCount.text = _remainigAmmo.ToString();
                    break;
                case PlayerLabel.Four:
                    Player4BulletCount.text = _remainigAmmo.ToString();
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Quando viene richiamata aggiorna i Kill point per il player che ha ucciso qualcuno e per la vittima
        /// </summary>
        /// <param name="_playerID">Il giocatore a cui aggiornare i punti uccisione nella Ui</param>
        /// <param name="_playerPoints">I punti da visualizzare nella UI</param>
        public void SetKillPointsUI(PlayerLabel _playerID, int _playerPoints)
        {
            switch (_playerID)
            {
                case PlayerLabel.One:
                    Player1KillPoints.text = _playerPoints.ToString();
                    break;                   
                case PlayerLabel.Two:        
                    Player2KillPoints.text = _playerPoints.ToString();
                    break;                   
                case PlayerLabel.Three:      
                    Player3KillPoints.text = _playerPoints.ToString();
                    break;                   
                case PlayerLabel.Four:       
                    Player4KillPoints.text = _playerPoints.ToString();
                    break;
                default:
                    break;
            }
        }

        public void SetElementZeroSlider(float _life, float _maxLife)
        {
            ElementZeroSlider.value = _life / _maxLife;                  // Da rivedere se il valore della vita cambia
        }

        /// <summary>
        /// Aggiorna il numero del livello e del round visualizzati durante il round
        /// </summary>
        public void UpdateLevelInformation()
        {
            LevelIndicationText.text = "Level: " + GameManager.Instance.LevelMng.levelNumber + "/" + "Round: " + GameManager.Instance.LevelMng.roundNumber;
        }

        #endregion
    }
}
