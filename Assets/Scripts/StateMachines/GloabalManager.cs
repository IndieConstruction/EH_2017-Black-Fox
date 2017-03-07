using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class GloabalManager : BaseStateMachine
    {


        void Start()
        {
            CurrentState = new MainMenuState();
        }


        void Update()
        {

        }
    }
}
