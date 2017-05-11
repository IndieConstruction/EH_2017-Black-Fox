using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class PlayerUpgradeController : BaseMenu
    {
        public UpgradeControllerID MenuID;

        public List<IUpgrade> Upgrades
        {
            get { return Player.Avatar.Upgrades; }
        }


        public void Init()
        {
            FindISelectableChildren();

            for (int i = 0; i < SelectableButtons.Count && i < Upgrades.Count; i++) {
                (SelectableButtons[i] as ISelectableUpgrade).SetIUpgrade(Upgrades[i]);
            }
        }

        public override void Selection()
        {
            for (int i = 0; i < Upgrades.Count; i++) {
                Upgrades[i] = (SelectableButtons[i] as ISelectableUpgrade).GetData();
            }
        }

        public override void GoRightInMenu(Player _player)
        {
            SelectableUpgrade currentSlider = selectableButton[currentIndexSelection] as SelectableUpgrade;
            currentSlider.AddValue();
        }

        public override void GoLeftInMenu(Player _player)
        {
            SelectableUpgrade currentSlider = selectableButton[currentIndexSelection] as SelectableUpgrade;
            currentSlider.RemoveValue();
        }
    }

    /// <summary>
    /// Serve per associare al upgrade controller il player corrispondente
    /// </summary>
    public enum UpgradeControllerID
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }
}