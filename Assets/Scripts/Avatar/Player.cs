using System.Collections;
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
        /// <summary>
        /// Scheletro su cui costruire/setuppare l'Avatar
        /// </summary>
        protected GameObject shipPlaceHolder;

        private Avatar _avatar;
        public Avatar Avatar
        {
            get {
                if (_avatar == null)
                {
                    shipPlaceHolder = Resources.Load<GameObject>("Prefabs/Avatar/ShipBase");
                    _avatar = Instantiate(shipPlaceHolder).GetComponent<Avatar>();
                }
                return _avatar; }
            protected set { _avatar = value; }
        }

        private ShipModel _model;
        public ShipModel Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public float SpawnTime = 0;

        protected void Update()
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
        #endregion

        #region PlayerSM
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
        #endregion

        #region Input
        //Input fields
        public InputStatus inputStatus;
        PlayerInput playerInput;
        bool isReleased = true;

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