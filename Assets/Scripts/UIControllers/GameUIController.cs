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
        public Image WindDisplay;
        public Text TextWindDisplay;
        public Slider ElementZeroSlider;

        public TMP_Text LevelText;
        LevelManager levelMNG;

        private void OnEnable()
        {
            EventManager.OnLevelInit += UpdateLevelInformation;
        }

        private void Start()
        {
            levelMNG = FindObjectOfType<LevelManager>();
        }

        void UpdateLevelInformation()
        {
            LevelText.text = "Level: " + levelMNG.lelvelNumber + "/" + "Round: " + levelMNG.roundNumber;
        }

        #region API
        public void SetBulletsValue(PlayerIndex _playerIndex, int _remainigAmmo)
        {
            if (_playerIndex == PlayerIndex.One)
            {
                Player1BulletCount.text = _remainigAmmo.ToString();
            }
            else if (_playerIndex == PlayerIndex.Two)
            {
                Player2BulletCount.text = _remainigAmmo.ToString();
            }
            else if (_playerIndex == PlayerIndex.Three)
            {
                Player3BulletCount.text = _remainigAmmo.ToString();
            }
            else if (_playerIndex == PlayerIndex.Four)
            {
                Player4BulletCount.text = _remainigAmmo.ToString();
            }
        }
                   

        public void SetElementZeroSlider(float _life, float _maxLife)
        {
            ElementZeroSlider.value = _life / _maxLife;                  // Da rivedere se il valore della vita cambia
        }

        public void ShowWinner(PlayerIndex _playerIndex)
        {
            WindDisplay.gameObject.SetActive(true);
            TextWindDisplay.text = "Player" + _playerIndex + " Ha vinto! ";
        }

        #endregion 
    }
}
