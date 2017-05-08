using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{

    public class UpgradePlayerController : BaseMenu
    {
        public NumberOfMenuPlayer menuOfPlayer;
        public PlayerIndex player;

        // Use this for initialization
        void Start()
        {
            FindISelectableChildren();
            CheckInput();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void CheckInput()
        {
            if (menuOfPlayer == (NumberOfMenuPlayer)player)
            {
                ///Avento 4 player attivi contemporaneamente ognuno dovrà interagire solo con il proprio menu di upgrade
            }
        }

        public enum NumberOfMenuPlayer
        {
            One,
            Two,
            Three,
            Four
        }

    }
}