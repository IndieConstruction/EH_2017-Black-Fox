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
        public void SetBulletsValue(Avatar _avatar)
        {
            switch (_avatar.PlayerId)   
            {
                case PlayerLabel.One:
                    Player1BulletCount.text = _avatar.ship.Shooter.ammo.ToString();
                    break;
                case PlayerLabel.Two:
                    Player2BulletCount.text = _avatar.ship.Shooter.ammo.ToString();
                    break;
                case PlayerLabel.Three:
                    Player3BulletCount.text = _avatar.ship.Shooter.ammo.ToString();
                    break;
                case PlayerLabel.Four:
                    Player4BulletCount.text = _avatar.ship.Shooter.ammo.ToString();
                    break;
                default:
                    break;
            }
        }
        

        /// <summary>
        /// Quando viene richiamata va a leggere i punti del player richiesto nel levelMng e li aggiorna nella UI.
        /// </summary>
        /// <param name="_playerID">Il giocatore a cui aggiornare i punti uccisione nella Ui</param>
        public void SetKillPointsUI(PlayerLabel _playerID)
        {
            switch (_playerID)
            {
                case PlayerLabel.One:
                    Player1KillPoints.text = GameManager.Instance.LevelMng.GetPlayerKillPoints(_playerID).ToString();
                    break;                   
                case PlayerLabel.Two:        
                    Player2KillPoints.text = GameManager.Instance.LevelMng.GetPlayerKillPoints(_playerID).ToString();
                    break;                   
                case PlayerLabel.Three:      
                    Player3KillPoints.text = GameManager.Instance.LevelMng.GetPlayerKillPoints(_playerID).ToString();
                    break;                   
                case PlayerLabel.Four:       
                    Player4KillPoints.text = GameManager.Instance.LevelMng.GetPlayerKillPoints(_playerID).ToString();
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
