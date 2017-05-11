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

        public override void Selection(Player _player)
        {
            switch (CurrentIndexSelection)
            {
                case 0:
                    GameManager.Instance.flowSM.SetPassThroughOrder(new List<StateBase>() { new GameplayState() });
                    break;
                case 1:
                    GameManager.Instance.flowSM.SetPassThroughOrder(new List<StateBase>() { new MainMenuState() });
                    break;
                default:
                    break;
            }
        }
    }
}