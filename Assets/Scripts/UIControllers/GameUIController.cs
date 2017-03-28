﻿using System.Collections;
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
        public Slider ElementZeroSlider;

        public TMP_Text LevelText;
        LevelManager levelMNG;

        

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
