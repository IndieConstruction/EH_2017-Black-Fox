using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
namespace BlackFox
{
    public class PlayerInput
    {
        // Variabili per il funzionamento dei controller
        PlayerIndex playerIndex;
        GamePadState state;
        GamePadState prevState;
    
        public PlayerInput(PlayerIndex _playerIndex)
        {
            playerIndex = _playerIndex;
        }

        #region API
        public InputStatus GetPlayerInputStatus()
        {
            InputStatus inputStatus = ControllerInput();
            if(!inputStatus.IsConnected)
                inputStatus = KeyboardInput();

            return inputStatus;
        }
        #endregion

        #region XInput
        /// <summary>
        /// Controlla l'input da controller (usando il plugin XInputDotNetPure)
        /// </summary>
        InputStatus ControllerInput()
        {
            InputStatus inputStatus = new InputStatus();

            if (!state.IsConnected)
            {
                inputStatus.IsConnected = state.IsConnected;
                return inputStatus;
            }
            else
                inputStatus.IsConnected = state.IsConnected;

            prevState = state;
            state = GamePad.GetState(playerIndex);

            inputStatus.RightTriggerAxis = state.Triggers.Right;
            inputStatus.LeftThumbSticksAxisX = state.ThumbSticks.Left.X;
            inputStatus.LeftThumbSticksAxisY = state.ThumbSticks.Left.Y;

            if (prevState.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Released && state.Buttons.RightShoulder == XInputDotNetPure.ButtonState.Pressed)
            {
                inputStatus.RightShoulder = ButtonState.Pressed;
            }

            if (prevState.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Released && state.Buttons.LeftShoulder == XInputDotNetPure.ButtonState.Pressed)
            {
                inputStatus.LeftShoulder = ButtonState.Pressed;
            }

            if (prevState.Buttons.A == XInputDotNetPure.ButtonState.Released && state.Buttons.A == XInputDotNetPure.ButtonState.Pressed)
            {
                inputStatus.A = ButtonState.Pressed;
            }

            if (prevState.Buttons.A == XInputDotNetPure.ButtonState.Pressed && state.Buttons.A == XInputDotNetPure.ButtonState.Pressed)
            {
                inputStatus.A = ButtonState.Held;
            }

            if (prevState.DPad.Up == XInputDotNetPure.ButtonState.Released && prevState.DPad.Up == XInputDotNetPure.ButtonState.Pressed)
            {
                inputStatus.DPadUp = ButtonState.Pressed;
            }

            if (prevState.DPad.Left == XInputDotNetPure.ButtonState.Released && prevState.DPad.Left == XInputDotNetPure.ButtonState.Pressed)
            {
                inputStatus.DPadLeft = ButtonState.Pressed;
            }

            if (prevState.DPad.Down == XInputDotNetPure.ButtonState.Released && prevState.DPad.Down == XInputDotNetPure.ButtonState.Pressed)
            {
                inputStatus.DPadDown = ButtonState.Pressed;
            }

            if (prevState.DPad.Right == XInputDotNetPure.ButtonState.Released && prevState.DPad.Right == XInputDotNetPure.ButtonState.Pressed)
            {
                inputStatus.DPadRight = ButtonState.Pressed;
            }

            return inputStatus;
        }
        #endregion

        #region KeyboardInput
        /// <summary>
        /// Controlla l'input da tastiera
        /// </summary>
        InputStatus KeyboardInput()
        {
            InputStatus inputStatus = new InputStatus();
            inputStatus.RightTriggerAxis = Input.GetAxis("Key" + (int)playerIndex + "_Forward");
            inputStatus.LeftThumbSticksAxisX = Input.GetAxis("Key" + (int)playerIndex + "_Horizonatal");

            if (Input.GetButtonDown("Key" + (int)playerIndex + "_PlaceRight"))
            {
                inputStatus.RightShoulder = ButtonState.Pressed;
            }

            if (Input.GetButtonDown("Key" + (int)playerIndex + "_PlaceLeft"))
            {
                inputStatus.LeftShoulder = ButtonState.Pressed;
            }

            if (Input.GetButtonDown("Key" + (int)playerIndex + "_Fire"))
            {
                inputStatus.A = ButtonState.Pressed;
            }

            if (Input.GetButton("Key" + (int)playerIndex + "_Fire"))
            {
                inputStatus.A = ButtonState.Held;
            }

            if (Input.GetButton("DPadUp"))
            {
                inputStatus.DPadUp = ButtonState.Pressed;
            }

            if (Input.GetButton("DPadLeft"))
            {
                inputStatus.DPadLeft = ButtonState.Pressed;
            }

            if (Input.GetButton("DPadDown"))
            {
                inputStatus.DPadDown = ButtonState.Pressed;
            }

            if (Input.GetButton("DPadRight"))
            {
                inputStatus.DPadRight = ButtonState.Pressed;
            }

            return inputStatus;
        }
        #endregion
    }

    /// <summary>
    /// Stato del bottone
    /// </summary>
    public enum ButtonState
    {
        Released = 0,
        Pressed = 1,
        Held = 2       
    }

    /// <summary>
    /// Strutta che contine tutti i comandi del joystick
    /// </summary>
    public struct InputStatus
    {
        public bool IsConnected;

        public float LeftTriggerAxis;
        public float RightTriggerAxis;

        public float LeftThumbSticksAxisX;
        public float LeftThumbSticksAxisY;

        public float RightThumbSticksAxisX;
        public float RightThumbSticksAxisY;

        public ButtonState A;
        public ButtonState B;
        public ButtonState X;
        public ButtonState Y;

        public ButtonState LeftShoulder;
        public ButtonState RightShoulder;

        public ButtonState LeftThumbSticks;
        public ButtonState RightThumbSticks;

        public ButtonState DPadUp;
        public ButtonState DPadLeft;
        public ButtonState DPadDown;
        public ButtonState DPadRight;

        public ButtonState Start;
        public ButtonState Select;
    }
}