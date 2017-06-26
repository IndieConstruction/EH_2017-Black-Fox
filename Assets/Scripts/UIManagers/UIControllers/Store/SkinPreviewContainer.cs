using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace BlackFox {

public class SkinPreviewContainer : BaseMenu
    {
        List<Image> Images = new List<Image>();
        StoreController storeController;

        int _IndexSelected;

        public int IndexSelected
        {
            get { return _IndexSelected; }
            set
            {
                _IndexSelected = value;
                if (_IndexSelected > 4 - 1)
                    _IndexSelected = 0;

                if (_IndexSelected < 0)
                    _IndexSelected = 4 - 1;

                storeController.MoveActiveImage(Images[IndexSelected].rectTransform);

            }
        }

        public void Init(StoreController _controller)
        {
            storeController = _controller;
            Images = GetComponentsInChildren<Image>().ToList();
            Images.Remove(Images[Images.Count - 1]);
        }

        #region Menu Action

        public override void GoUpInMenu(Player _player)
        {
            IndexSelected--;
        }

        public override void GoDownInMenu(Player _player)
        {
            IndexSelected++;
        }

        public override void Selection(Player _player)
        {
            base.Selection(_player);
        }
        

        #endregion
    }
}