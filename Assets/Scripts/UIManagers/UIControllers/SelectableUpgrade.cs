using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{

    public class SelectableUpgrade : MonoBehaviour, ISelectableUpgrade
    {
        #region ISelectable

        bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                CheckIsSelected(isSelected);
            }
        }

        int index;

        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        Image SelectionImage;

        public void SetIndex(int _index)
        {
            Index = _index;
        }

        public void CheckIsSelected(bool _isSelected)
        {
            SelectionImage = GetComponentInChildren<Image>();

            if (_isSelected)
                SelectionImage.color = Color.red;
            else
                SelectionImage.color = Color.white;
        }
        #endregion

        public Slider slider;
        public Text text;

        public IUpgrade Upgrade;

        public void AddValue()
        {
            if (Upgrade.CurrentUpgradeLevel < Upgrade.MaxLevel)
            {
                slider.value += 1;
                Upgrade.CurrentUpgradeLevel += 1;
            }
        }

        public void RemoveValue()
        {
            if(Upgrade.CurrentUpgradeLevel > Upgrade.MinLevel)
            {
                slider.value -= 1;
                Upgrade.CurrentUpgradeLevel -= 1;
            }
        }

        public void SetIUpgrade(IUpgrade _upgrade)
        {
            Upgrade = _upgrade;
            Upgrade.CurrentUpgradeLevel = Upgrade.MinLevel;
            slider.value = Upgrade.CurrentUpgradeLevel;
            slider.maxValue = Upgrade.MaxLevel;
            text.text = Upgrade.ID.ToString();
        }

        public IUpgrade GetData()
        {
            Upgrade.MinLevel = Upgrade.CurrentUpgradeLevel;
            return Upgrade;
        }
    }
}