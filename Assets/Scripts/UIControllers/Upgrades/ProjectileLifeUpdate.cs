using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class ProjectileLifeUpdate : MonoBehaviour, IUpgrade, ISelectable {


        #region Variables

        public int CurrentUpgradeLevel
        {
            get { return PlayerUpgradeController.Avatar.ProgectileUpgradeLevel ; }

            set
            {
                // prima di fare il set controllare che la fase di upgrade sia terminata
                PlayerUpgradeController.Avatar.ProgectileUpgradeLevel = value;
            }
        }

        int _maxLevel;

        public int MaxLevel
        {
            get { return _maxLevel; }

            set { _maxLevel = value; }
        }

        int _minLevel;

        public int MinLevel
        {
            get { return _minLevel; }

            set { _minLevel = value; }
        }

        private PlayerUpgradeController _playerUpgradeController;

        public PlayerUpgradeController PlayerUpgradeController
        {
            get { return _playerUpgradeController; }

            set { _playerUpgradeController = value; }
        }

        int index;

        public int Index
        {
            get { return index; }

            set { index = value; }
        }

        bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }

            set { isSelected = value; }
        }

        Image selectionImage;

        Slider slide;

        #endregion


        /// <summary>
        /// Da richiamare ogni volta che il pannello viene riaperto
        /// </summary>
        void OnActivation()
        {
            MinLevel = CurrentUpgradeLevel;
            selectionImage = GetComponentInChildren<Image>();
            slide = GetComponentInChildren<Slider>();
            slide.value = MinLevel;
        }

        public void ApplyUpgrade()
        {
            /// Applica il potenziamento (setta il current level)
        }


        public void SetIndex(int _index)
        {
            Index = _index;
        }

        public void CheckIsSelected(bool _isSelected)
        {
            if (IsSelected)
                selectionImage.color = Color.red;
            else
                selectionImage.color = Color.white;
        }
    }
}