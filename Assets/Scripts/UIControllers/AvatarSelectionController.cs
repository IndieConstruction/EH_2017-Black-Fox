using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class AvatarSelectionController : BaseMenu {

        AvatarSelectionManager avatarSelectionManager;

        public UIControllerID MenuID;

        public void Setup(AvatarSelectionManager _avatarSelectionManager, Player _player) {
            avatarSelectionManager = _avatarSelectionManager;
            Player = _player;
        }


        public override void GoRightInMenu(Player _player) {
            GameManager.Instance.SRMng.rooms[(int)_player.ID - 1].ShowNext();
        }

        public override void GoLeftInMenu(Player _player) {
            GameManager.Instance.SRMng.rooms[(int)_player.ID - 1].ShowPrevious();
        }
    }
}