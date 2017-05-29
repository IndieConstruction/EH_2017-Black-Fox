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

        #region Menu Actions
        public override void GoUpInMenu(Player _player)
        {
            foreach (AvatarSelectionController controller in avatarSelectionControllers)
            {
                if ((int)controller.MenuID == (int)_player.ID)
                {
                    controller.GoUpInMenu(_player);
                    break;
                }
            }

            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Movement);
        }

        public override void GoDownInMenu(Player _player)
        {
            foreach (AvatarSelectionController controller in avatarSelectionControllers)
            {
                if ((int)controller.MenuID == (int)_player.ID)
                {
                    controller.GoDownInMenu(_player);
                    break;
                }
            }

            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Movement);
        }

        public override void GoLeftInMenu(Player _player)
        {
            foreach (AvatarSelectionController controller in avatarSelectionControllers)
            {
                if ((int)controller.MenuID == (int)_player.ID)
                {
                    controller.GoLeftInMenu(_player);
                    break;
                }
            }

            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Movement);
        }

        public override void GoRightInMenu(Player _player)
        {
            foreach (AvatarSelectionController controller in avatarSelectionControllers)
            {
                if ((int)controller.MenuID == (int)_player.ID)
                {
                    controller.GoRightInMenu(_player);
                    break;
                }
            }

            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Movement);
        }

        public override void Selection(Player _player)
        {
            foreach (AvatarSelectionController controller in avatarSelectionControllers)
            {
                if ((int)controller.MenuID == (int)_player.ID)
                {
                    controller.Selection(_player);
                    break;
                }
            }

            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Selection);
        }

        public override void GoBack(Player _player)
        {
            foreach (AvatarSelectionController controller in avatarSelectionControllers)
            {
                if (_player.ID == PlayerLabel.One)
                {
                    if (EventManager.OnMenuAction != null)
                        EventManager.OnMenuAction(AudioManager.UIAudio.Back);

                    GameManager.Instance.flowSM.SetPassThroughOrder(new List<StateBase>() { new LevelSelectionState() });
                    break;
                }
            }
        }
        #endregion
    }
}