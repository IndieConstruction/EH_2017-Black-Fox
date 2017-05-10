using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class PlayerUpgradeController : BaseMenu
    {
        public UpgradeControllerID MenuID;

        List<IUpgrade> upgradeItem = new List<IUpgrade>();

        public void OnStart()
        {
            //foreach (IUpgrade item in GetComponentsInChildren<IUpgrade>())
            //{
            //    item.PlayerUpgradeController = this;
            //}
            FindISelectableChildren();
        }

        //TODO: completare facendo da intermediario fra gli IUpgrade e l'avatar
        public void ApplyUpgrade(IUpgrade _upgrade)
        {
            
        }

        public override void Selection()
        {
            
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