using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BlackFox
{
    public class MainMenuController : BaseMenu
    {
        public RectTransform PanelTransform;
        public void Init()
        {
            GameManager.Instance.UiMng.CurrentMenu = this;
            FindISelectableChildren();
            foreach (ISelectable button in SelectableButtons)
            {
                (button as SelectableButton).Init(GameManager.Instance.UiMng.SelectedButton, GameManager.Instance.UiMng.DeselectionButton);
            }

            SelectableButtons[0].IsSelected = true;
        }

        public override void GoDownInMenu(Player _player)
        {
            base.GoDownInMenu(_player);
            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Movement);
        }

        public override void GoUpInMenu(Player _player)
        {
            base.GoUpInMenu(_player);
            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Movement);
        }

        public override void Selection(Player _player)
        {
            switch (CurrentIndexSelection)
            {
                case 0:
                    // Cambia stato in level selection
                    PanelTransform.DORotate(Vector3.up * 90, 0.8f, RotateMode.LocalAxisAdd).OnComplete(() =>
                    {
                        GameManager.Instance.flowSM.SetPassThroughOrder(new List<StateBase>() { new LevelSelectionState() });
                    });
                    
                    break;
                case 1:
                    GameManager.Instance.QuitApplication();
                    break;
                case 2:
                    break;
            }

            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Selection);
        }
    }
}