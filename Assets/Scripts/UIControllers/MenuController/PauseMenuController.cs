﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class PauseMenuController : BaseMenu
    {        
        public GameObject ChildrenPanel;

        private void Start()
        {
            ChildrenPanel.SetActive(false);
            FindISelectableChildren();
        }

        public override void FindISelectableChildren()
        {
            foreach (ISelectable item in ChildrenPanel.GetComponentsInChildren<ISelectable>())
            {
                SelectableButtons.Add(item);
            }

            for (int i = 0; i < selectableButton.Count; i++)
            {
                selectableButton[i].SetIndex(i);
            }

            selectableButton[0].IsSelected = true;
        }

        public override void Selection()
        {
            switch (CurrentIndexSelection)
            {
                case 0:
                    GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
                    break;
                case 1:
                    GameManager.Instance.LevelMng.gameplaySM.GoToState(GamePlaySMStates.GameOverState);
                    break;
            }
        }
    }
}