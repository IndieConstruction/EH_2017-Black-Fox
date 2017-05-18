using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BlackFox {
    public class AvatarSelectionController : BaseMenu {

        AvatarSelectionManager avatarSelectionManager;

        public Text ConfirmText;

        public UIControllerID MenuID;


        UpgradeControllerState _currentState;
        public UpgradeControllerState CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                if (_currentState == UpgradeControllerState.Ready)
                {
                    avatarSelectionManager.CheckUpgradeControllersState();
                    GameManager.Instance.PlayerMng.ChangePlayerState(PlayerState.Blocked, Player.ID);
                    ConfirmText.text = "Ready";
                }
            }
        }

        public void Setup(AvatarSelectionManager _avatarSelectionManager, Player _player) {
            avatarSelectionManager = _avatarSelectionManager;
            Player = _player;
            CurrentState = UpgradeControllerState.Unready;
            ConfirmText.text = "Press A to continue";
        }


        public override void GoRightInMenu(Player _player) {
            GameManager.Instance.SRMng.rooms[(int)_player.ID - 1].ShowNext();
        }

        public override void GoLeftInMenu(Player _player) {
            GameManager.Instance.SRMng.rooms[(int)_player.ID - 1].ShowPrevious();
        }

        public override void GoUpInMenu(Player _player)
        {
            //Change the color.
        }

        public override void GoDownInMenu(Player _player)
        {
            //Change the color.
        }

        public override void Selection(Player _player)
        {
            CurrentState = UpgradeControllerState.Ready;
            //Save the scriptable in the avatar
        }
    }
}