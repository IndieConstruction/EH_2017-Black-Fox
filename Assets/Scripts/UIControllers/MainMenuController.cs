using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class MainMenuController : BaseMenu
    {
        void Start()
        {
            FindISelectableChildren();
            GameManager.Instance.UiMng.CurrentMenu = this;
        }

        public override void Selection()
        {
            switch (CurrentIndexSelection)
            {
                case 0:
                    // Cambia stato in level selection
                    GameManager.Instance.flowSM.SetPassThroughOrder(new List<StateBase>() { new LevelSelectionState() });
                    break;
                case 1:
                    // Apre i credits
                    break;
                case 2:
                    GameManager.Instance.QuitApplication();
                    break;
            }
        }
    }
}