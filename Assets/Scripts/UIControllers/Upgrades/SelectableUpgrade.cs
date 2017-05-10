using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{

    public class SelectableUpgrade : MonoBehaviour, ISelectable
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

        private PlayerUpgradeController _playerUpgradeController;

        public PlayerUpgradeController PlayerUpgradeController
        {
            get { return _playerUpgradeController; }

            set { _playerUpgradeController = value; }
        }

        //TODO: aggiungere la funzione AddValue per la slider.
    }
}