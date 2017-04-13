using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class Player
    {
        public PlayerIndex playerIndex;
        public string Name;
        public InputStatus inputStatus;

        PlayerState playerCurrentState;
        /// <summary>
        /// Stato attuale.
        /// </summary>
        public PlayerState PlayerCurrentState
        {
            get { return playerCurrentState; }
            set
            {
                if (playerCurrentState != value)
                    onStateChanged(playerCurrentState, value);
                playerCurrentState = value;
            }
        }

        /// <summary>
        /// Accade ogni volta che cambia stato.
        /// </summary>
        void onStateChanged(PlayerState _oldState, PlayerState _newState)
        {
            switch (_newState)
            {
                case PlayerState.Blocked:
                    inputStatus = new InputStatus();
                    break;
                case PlayerState.MenuInputState:
                    inputStatus = new InputStatus();
                    break;
                case PlayerState.PlayInputState:
                    break;
            }
        }

        PlayerInput playerInput;
        bool isReleased = true;


        public Player(PlayerIndex _playerIndex)
        {
            playerIndex = _playerIndex;
            playerInput = new PlayerInput(playerIndex);
        }

        /// <summary>
        /// Funzione sostitutiva all'update
        /// </summary>
        public void OnUpdate()
        {
            switch (PlayerCurrentState)
            {
                case PlayerState.Blocked:
                    // Stato in cui i comandi del player sono ingorati
                    break;
                case PlayerState.MenuInputState:
                    CheckMenuInputStatus(playerInput.GetPlayerInputStatus());
                    break;
                case PlayerState.PlayInputState:
                    inputStatus = playerInput.GetPlayerInputStatus();
                    break;
            }
        }


        /// <summary>
        /// Controlla l'inpunt da passare al menù corrente 
        /// </summary>
        /// <param name="_inputStatus"></param>
        void CheckMenuInputStatus(InputStatus _inputStatus)
        {
            if (_inputStatus.LeftThumbSticksAxisY <= 0.2 && _inputStatus.LeftThumbSticksAxisY >= -0.2)
                isReleased = true;

            if ((_inputStatus.DPadUp == ButtonState.Pressed || _inputStatus.LeftThumbSticksAxisY >= 0.5) && isReleased)
            {
                isReleased = false;
                GameManager.Instance.UiMng.GoUpInMenu();
            }

            if ((_inputStatus.DPadDown == ButtonState.Pressed || _inputStatus.LeftThumbSticksAxisY <= -0.5) && isReleased)
            {
                isReleased = false;
                GameManager.Instance.UiMng.GoDownInMenu();
            }

            if (_inputStatus.A == ButtonState.Pressed)
            {
                GameManager.Instance.UiMng.SelectInMenu();
            }

            if (_inputStatus.B == ButtonState.Pressed)
            {
                // TODO : call go back in menù
            }
        }
    }
}