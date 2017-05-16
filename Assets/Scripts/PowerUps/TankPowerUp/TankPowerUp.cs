using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class TankPowerUp : PowerUpBase {
        public float PowerUpDuration = 10;

        public override void UsePowerUp()
        {
            DamagingShell tempShell = (collector as Avatar).ship.gameObject.AddComponent<DamagingShell>();
            tempShell.Init(PowerUpDuration);
        }
    }
}