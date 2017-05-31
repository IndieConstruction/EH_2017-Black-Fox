using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class TankPowerUp : PowerUpBase {

        public int PowerUpDamage;

        public override void UsePowerUp()
        {
            DamagingShell tempShell = (collector as Avatar).ship.gameObject.AddComponent<DamagingShell>();
            tempShell.Init(PowerUpDuration, PowerUpDamage);
        }
    }
}