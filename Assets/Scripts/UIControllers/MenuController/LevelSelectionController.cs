using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BlackFox
{
    public class LevelSelectionController : BaseMenu
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
                    GameManager.Instance.flowSM.GoToState(FlowSMStates.GameplayState);
                    break;
                case 1:
                    GameManager.Instance.flowSM.GoToState(FlowSMStates.MainMenuState);
                    break;
                default:
                    break;
            }
        }
    }
}