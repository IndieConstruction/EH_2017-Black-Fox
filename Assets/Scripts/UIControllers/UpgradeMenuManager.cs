using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox { 
    public class UpgradeMenuManager : BaseMenu
    {
        public GameObject UpgradePanel;

        public List<PlayerUpgradeController> PlayerUpgradeControllers = new List<PlayerUpgradeController>();

        private void Start()
        {
            UpgradePanel.SetActive(false);
        }

        public void OnStart()
        {
            foreach (PlayerUpgradeController controller in PlayerUpgradeControllers)
            {
                foreach (Player player in GameManager.Instance.PlayerMng.Players)
                {
                    if ((int)player.ID == (int)controller.MenuID)
                    {
                        controller.Avatar = player.Avatar;
                        controller.OnStart();
                        break;
                    }
                }
            }
        }
        
        public override void GoUpInMenu(Player _player)
        {
            foreach (PlayerUpgradeController menu in PlayerUpgradeControllers)
            {
                if ((int)menu.MenuID == (int)_player.ID)
                    menu.GoUpInMenu(_player);
            }
        }

        public override void GoDownInMenu(Player _player)
        {
            foreach (PlayerUpgradeController menu in PlayerUpgradeControllers)
            {
                if ((int)menu.MenuID == (int)_player.ID)
                    menu.GoDownInMenu(_player);
            }
        }

        public override void GoLeftInMenu(Player _player)
        {
            foreach (PlayerUpgradeController menu in PlayerUpgradeControllers)
            {
                if ((int)menu.MenuID == (int)_player.ID)
                    menu.GoLeftInMenu(_player);
            }
        }

        public override void GoRightInMenu(Player _player)
        {
            foreach (PlayerUpgradeController menu in PlayerUpgradeControllers)
            {
                if ((int)menu.MenuID == (int)_player.ID)
                    menu.GoRightInMenu(_player);
            }
        }
    }
}