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

        InputController inputCtrl;

        private void Start()
        {
            inputCtrl = GetComponent<InputController>();
            inputCtrl.SetPlayerIndex(playerIndex);
        }

    }
}