using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class UIManager : MonoBehaviour {

        MenuUIController menuUIController;
        GameUIController gameUIController;

        void Start() {

        }

        void Update() {

        }

        #region MenuUIController
        public void SetMenuUIController(MenuUIController _menuUI)
        {
            menuUIController = _menuUI;
        }

        public MenuUIController GetMenuUIController()
        {
            return menuUIController;
        }
        #endregion

        #region GameUIController
        public void SetGameUIController(GameUIController _gameUI)
        {
            gameUIController = _gameUI;
        }

        public GameUIController GetGameUIController()
        {
            return gameUIController;
        }

        public void SliderValueUpdate(PlayerIndex _playerIndex, float _life)
        {
            gameUIController.SetSliderValue(_playerIndex, _life);
        }

        public void BullletsValueUpdate(PlayerIndex _playerIndex, int _remainigAmmo)
        {
            gameUIController.SetBulletsValue(_playerIndex, _remainigAmmo);
        }

        public void CoreSliderValueUpdate(float _life, float _maxLife)
        {
            gameUIController.SetCoreSliderValue(_life, _maxLife);
        }

        public void ElementZeroValueUpdate(float _life, float _maxLife)
        {
            gameUIController.SetElementZeroSlider(_life, _maxLife);
        }

        public void DisplayWinnerPlayer(PlayerIndex _playerIndex)
        {
            gameUIController.ShowWinner(_playerIndex);
        }
        #endregion
    }
}