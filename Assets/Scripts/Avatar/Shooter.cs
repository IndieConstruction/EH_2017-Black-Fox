using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class Shooter : ShooterBase
    {
        public int AddedAmmo = 10;
        public int MaxAmmo = 50;
        public int ammo = 0;
        [HideInInspector]
        public PlayerIndex playerIndex;

        /// <summary>
        /// Spara un proiettile
        /// </summary>
        public override void ShootBullet()
        {
            if (ammo > 0)
            {
                base.ShootBullet();
                ammo--;
            }
        }

        public void AddAmmo()
        {
            if (ammo < MaxAmmo)
                ammo += AddedAmmo;
            else if (ammo > MaxAmmo)
                ammo = AddedAmmo;
        }
    }
}
