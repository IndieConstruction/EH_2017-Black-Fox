using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class AvatarSelectionManager : BaseMenu {

        public GameObject AvatarSelectionPanel;

        public List<AvatarSelectionController> avatarSelectionControllers = new List<AvatarSelectionController>();


        public void Setup(List<Player> _players) {
            foreach (Player player in _players) {
                foreach (AvatarSelectionController controller in avatarSelectionControllers) {
                    if ((int)player.ID == (int)controller.MenuID) {
                        controller.Setup(this, player);
                        //controller.FindISelectableChildren();
                        break;
                    }
                }
            }
            //AvatarSelectionPanel.SetActive(false);
        }


        public void CheckUpgradeControllersState()
        {
            foreach (AvatarSelectionController controller in avatarSelectionControllers)
            {
                if (controller.CurrentState == AvatarSelectionControllerState.Unready)
                    return;
            }
            GameManager.Instance.SRMng.LoadSelectedDatas();
            GameManager.Instance.flowSM.SetPassThroughOrder(new List<StateBase>() { new GameplayState() });
        }

        public override void GoUpInMenu(Player _player) {
            foreach (AvatarSelectionController controller in avatarSelectionControllers) {
                if ((int)controller.MenuID == (int)_player.ID) {
                    controller.GoUpInMenu(_player);
                    break;
                }
            }
        }

        public override void GoDownInMenu(Player _player) {
            foreach (AvatarSelectionController controller in avatarSelectionControllers) {
                if ((int)controller.MenuID == (int)_player.ID) {
                    controller.GoDownInMenu(_player);
                    break;
                }
            }
        }

        public override void GoLeftInMenu(Player _player) {
            foreach (AvatarSelectionController controller in avatarSelectionControllers) {
                if ((int)controller.MenuID == (int)_player.ID) {
                    controller.GoLeftInMenu(_player);
                    break;
                }
            }
        }

        public override void GoRightInMenu(Player _player) {
            foreach (AvatarSelectionController controller in avatarSelectionControllers) {
                if ((int)controller.MenuID == (int)_player.ID) {
                    controller.GoRightInMenu(_player);
                    break;
                }
            }
        }

        public override void Selection(Player _player) {
            foreach (AvatarSelectionController controller in avatarSelectionControllers) {
                if ((int)controller.MenuID == (int)_player.ID) {
                    controller.Selection(_player);
                    break;
                }
            }
        }

    }
}