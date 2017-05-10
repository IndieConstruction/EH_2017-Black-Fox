using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class GameUIController : MonoBehaviour
    {
        public Text[] PlayersBulletCount = new Text[4];

        public Text[] PlayersKillPoints = new Text[4];

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
            for (int i = 0; i < PlayersBulletCount.Length; i++)
            {
                if(_avatar.PlayerId == (PlayerLabel)i + 1)
                    PlayersBulletCount[i].text = _avatar.ship.shooter.Ammo.ToString();
            }
        }
        

        /// <summary>
        /// Quando viene richiamata va a leggere i punti del player richiesto nel levelMng e li aggiorna nella UI.
        /// </summary>
        /// <param name="_playerID">Il giocatore a cui aggiornare i punti uccisione nella Ui</param>
        public void SetKillPointsUI(PlayerLabel _playerID)
        {
            for (int i = 0; i < PlayersKillPoints.Length; i++)
            {
                if (_playerID == (PlayerLabel)i + 1)
                    PlayersKillPoints[i].text = GameManager.Instance.LevelMng.GetPlayerKillPoints(_playerID).ToString();
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
            LevelIndicationText.text = "Level: " + GameManager.Instance.LevelMng.LevelNumber + "/" 
                + "Round: " + GameManager.Instance.LevelMng.RoundNumber;
        }

        #endregion


        #region Events
        private void OnEnable()
        {
            EventManager.OnAmmoValueChange += SetBulletsValue;
        }

        private void OnDisable()
        {
            EventManager.OnAmmoValueChange -= SetBulletsValue;
        }
        #endregion
    }
}
