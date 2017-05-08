using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class AmmoCleaner : PowerUpBase {

        public override void UsePowerUp() {
            Avatar avatarColl = collector as Avatar;
            avatarColl.ship.shooter.Ammo = 0;
        }
    }
}
