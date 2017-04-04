using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
namespace BlackFox
{
    public class InputController : MonoBehaviour
    {

        // Variabili per il funzionamento dei controller
        PlayerIndex playerIndex;
        GamePadState state;
        GamePadState prevState;

        void FixedUpdate()
        {
            KeyboardReader();
            XInputReader();
        }

        #region API
        public void SetPlayerIndex(PlayerIndex _playerIndex)
        {
            playerIndex = _playerIndex;
        }
        #endregion

        #region XInput
        /// <summary>
        /// Controlla l'input da controller
        /// </summary>
        void XInputReader()
        {
            prevState = state;
            state = GamePad.GetState(playerIndex);

            //GoForward(state.Triggers.Right);
            //Rotate(state.ThumbSticks.Left.X);

            if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
            {

            }

            if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
            {

            }

            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                //nextFire = Time.time + fireRate;

            }
            else if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Pressed /*&& Time.time > nextFire*/)
            {
                //nextFire = Time.time + fireRate;

            }
        }
        #endregion
        #region KeyboardInput
        /// <summary>
        /// Controlla l'input da tastiera
        /// </summary>
        void KeyboardReader()
        {
            //Rotate(Input.GetAxis(string.Concat("Key" + (int)playerIndex + "_Horizonatal")));
            //GoForward(Input.GetAxis(string.Concat("Key" + (int)playerIndex + "_Forward")));

            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceRight")))
            {

            }

            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceLeft")))
            {


            }

            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_Fire")))
            {
                //nextFire = Time.time + fireRate;

            }

            if (Input.GetButton(string.Concat("Key" + (int)playerIndex + "_Fire")) /*&& Time.time > nextFire*/)
            {
                //nextFire = Time.time + fireRate;

            }
        }
        #endregion
    }
}