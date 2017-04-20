﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class Player : MonoBehaviour
    {
        protected PlayerLabel _ID;
        public PlayerLabel ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private Avatar _avatar;
        public Avatar Avatar
        {
            get { return _avatar; }
            protected set { _avatar = value; }
        }

        private AvatarData _model;
        public AvatarData Model
        {
            get {
                if (_model == null) {
                    _model = Instantiate(Resources.Load("Prefabs/ShipModels/Owl") as AvatarData);// TODO : controllare riempimento avatar
                    Avatar = Instantiate(_model.shipConfig.Prefab);
                }

                return _model; }
            set { _model = value; }
        }

        public float SpawnTime = 0;

        private void Update()
        {
            switch (PlayerCurrentState)
            {
                case PlayerState.Blocked:
                    break;
                case PlayerState.MenuInput:
                    InputStatus = playerInput.GetPlayerInputStatus();
                    CheckMenuInputStatus();
                    break;
                case PlayerState.PlayInput:
                    InputStatus = playerInput.GetPlayerInputStatus();
                    break;
                default:
                    break;
            }
        }

        #region API
        /// <summary>
        /// Mut be used to setup an istance of a Player
        /// </summary>
        /// <param name="_playerIndex"></param>
        public void Setup(PlayerLabel _playerID)
        {
            ID = _playerID;
            playerInput = new PlayerInput(_playerID);
        }

        /// <summary>
        /// Setup the istance of the Avatar or instaciate a new one
        /// </summary>
        public void AvatarSetup() {
            if (ID == PlayerLabel.None)
                return;
            Avatar.Setup(this);
        }

        /// <summary>
        /// Destroy the Avatar istance and the connected rope if there is one
        /// </summary>
        public void DestroyAvatar()
        {
            if (Avatar)
                Destroy(Avatar.gameObject);
        }
        #endregion

        #region PlayerSM
        PlayerState _playerCurrentState;
        /// <summary>
        /// Stato attuale.
        /// </summary>
        public PlayerState PlayerCurrentState
        {
            get { return _playerCurrentState; }
            set {
                if(_playerCurrentState != value)
                    StateChange(value, _playerCurrentState);
                _playerCurrentState = value;
            }
        }

        void StateChange(PlayerState _newState, PlayerState _oldState)
        {
            switch (_newState)
            {
                case PlayerState.Blocked:
                    InputStatus.Reset();
                    break;
                case PlayerState.MenuInput:
                    break;
                case PlayerState.PlayInput:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Input
        //Input fields
        PlayerInput playerInput;
        private InputStatus _inputStatus;
        public InputStatus InputStatus
        {
            get {
                if (_inputStatus == null)
                    _inputStatus = new InputStatus();
                return _inputStatus;
            }
            set { _inputStatus = value; }
        }

        bool isReleased = true;

        /// <summary>
        /// Controlla l'inpunt da passare al menù corrente 
        /// </summary>
        /// <param name="inputStatus"></param>
        void CheckMenuInputStatus()
        {
            if (InputStatus.LeftThumbSticksAxisY <= 0.2 && InputStatus.LeftThumbSticksAxisY >= -0.2)
                isReleased = true;

            if ((InputStatus.DPadUp == ButtonState.Pressed || InputStatus.LeftThumbSticksAxisY >= 0.5) && isReleased)
            {
                isReleased = false;
                GameManager.Instance.UiMng.GoUpInMenu();
            }

            if ((InputStatus.DPadDown == ButtonState.Pressed || InputStatus.LeftThumbSticksAxisY <= -0.5) && isReleased)
            {
                isReleased = false;
                GameManager.Instance.UiMng.GoDownInMenu();
            }

            if (InputStatus.A == ButtonState.Pressed)
            {
                GameManager.Instance.UiMng.SelectInMenu();
            }

            if (InputStatus.B == ButtonState.Pressed)
            {
                // TODO : call go back in menù
            }
        }

        public void ControllerVibration(float _leftMotor, float _rightMotor)
        {
            playerInput.SetControllerVibration(_leftMotor, _rightMotor);
        }
        #endregion

    }

    public enum PlayerLabel
    {
        None = 0,
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Different = 5
    }
}