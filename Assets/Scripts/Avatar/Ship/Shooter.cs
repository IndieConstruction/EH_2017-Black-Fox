using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class Shooter : ShooterBase
    {
        ShooterConfig shooterConfig
        {
            get
            {
                if (shooterBaseConfig == null)
                    shooterBaseConfig = ship.avatar.AvatarData.shipConfig.shooterConfig.ShooterBaseConfig;
                return ship.avatar.AvatarData.shipConfig.shooterConfig;
            }
        }

        int ammo;

        public int Ammo
        {
            get { return ammo; }
            set { ammo = value;
                ship.avatar.OnAmmoUpdate();
            }
        }

        Ship ship;

        #region API
        public void Init(Ship _ship)
        {
            ship = _ship;
        }

        public override void ShootBullet()
        {
            if (Ammo > 0)
            {
                base.ShootBullet();
                Ammo--;
            }
        }

        public override void SetFireDirection(Vector3 _direction)
        {
            if(_direction != Vector3.zero)
                direction = _direction;
        }

        public void AddAmmo()
        {
            if (Ammo < shooterConfig.MaxAmmo)
                Ammo += shooterConfig.AddedAmmo;
            else if (Ammo > shooterConfig.MaxAmmo)
                Ammo = shooterConfig.MaxAmmo;
        }

        public void AmmoCheat()
        {
            Ammo = 500;
            shooterConfig.MaxAmmo = 500;
        }
        #endregion
    }

    [Serializable]
    public class ShooterConfig
    {
        public ShooterBaseConfig ShooterBaseConfig;
        public int AddedAmmo;
        public int MaxAmmo;
    }
}
