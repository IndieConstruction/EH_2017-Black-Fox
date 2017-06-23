using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace BlackFox {
    public class ModelPreviewContainer : BaseMenu {

        List<RawImage> Images = new List<RawImage>();
        StoreController storeController;
        int _currentIndexSelected;

        public int CurrentIndexSelected
        {
            get { return _currentIndexSelected; }
            set
            {
                _currentIndexSelected = value;
                if (_currentIndexSelected > 4 - 1)
                    _currentIndexSelected = 0;

                if (_currentIndexSelected < 0)
                    _currentIndexSelected = 4 - 1;

                storeController.MoveActiveImage(Images[CurrentIndexSelected].rectTransform);

            }
        }

        public void Init(StoreController _controller)
        {
            storeController = _controller;
            Images = GetComponentsInChildren<RawImage>().ToList();
        }

        public override void GoUpInMenu(Player _player)
        {
            CurrentIndexSelected--;
        }

        public override void GoDownInMenu(Player _player)
        {
            CurrentIndexSelected++;
        }

        public override void Selection(Player _player)
        {
         
        }

        public override void GoBack(Player _player)
        {
         
        }
    }
}