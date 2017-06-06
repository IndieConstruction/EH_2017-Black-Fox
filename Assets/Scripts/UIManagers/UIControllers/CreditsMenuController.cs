using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class CreditsMenuController :  BaseMenu
    {
        public override void GoBack(Player _player)
        {
            GameManager.Instance.flowSM.SetPassThroughOrder(new List<StateBase>() { new MainMenuState() });
        }
    }
}