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
        public PlayerState PlayerCurrentState;

        PlayerInput playerInput;

        public Player(PlayerIndex _playerIndex)
        {
            playerIndex = _playerIndex;
            playerInput = new PlayerInput(playerIndex);
        }

        public void OnUpdate()
        {
            inputStatus = playerInput.GetPlayerInputStatus();
        }

    }
}