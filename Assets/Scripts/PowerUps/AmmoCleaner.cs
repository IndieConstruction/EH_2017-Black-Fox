using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class AmmoCleaner : PowerUpBase
    {
        public override void UsePowerUp()
        {
            foreach (IPowerUpCollector enemy in enemyCollectors)
            {
                (enemy as Avatar).ship.shooter.Ammo = 0;
            }
        }
    }
}
