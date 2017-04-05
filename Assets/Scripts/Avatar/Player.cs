using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class Player : MonoBehaviour
    {
        public PlayerIndex playerIndex;

        public string Name;

        public InputStatus inputStatus;

        PlayerInput playerInput;

        private void Start()
        {
            playerInput = new PlayerInput(playerIndex);
        }

        private void Update()
        {
            inputStatus = playerInput.GetPlayerInputStatus();
        }

    }
}