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

        public void OnStart(List<Player> _players)
        { 
            foreach (Player player in _players)
            {
                foreach (PlayerUpgradeController controller in PlayerUpgradeControllers)
                {
                    if ((int)player.ID == (int)controller.MenuID)
                    {
                        controller.Player = player;
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
                {
                    menu.GoUpInMenu(_player);
                    break;
                }
            }
        }

        public override void GoDownInMenu(Player _player)
        {
            foreach (PlayerUpgradeController menu in PlayerUpgradeControllers)
            {
                if ((int)menu.MenuID == (int)_player.ID)
                {
                    menu.GoDownInMenu(_player);
                    break;
                }
            }
        }

        public override void GoLeftInMenu(Player _player)
        {
            foreach (PlayerUpgradeController menu in PlayerUpgradeControllers)
            {
                if ((int)menu.MenuID == (int)_player.ID)
                {
                    menu.GoLeftInMenu(_player);
                    break;
                }
            }
        }

        public override void GoRightInMenu(Player _player)
        {
            foreach (PlayerUpgradeController menu in PlayerUpgradeControllers)
            {
                if ((int)menu.MenuID == (int)_player.ID)
                {
                    menu.GoRightInMenu(_player);
                    break;
                }
            }
        }

        public override void Selection()
        {
            
        }
    }
}