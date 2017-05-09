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
        Ship ship;
        public int Ammo
        {
            get { return ammo; }
            set { ammo = value; }
        }


        #region API
        public void Init(Ship _ship)
        {
            ship = _ship;
        }

        public override void ShootBullet()
        {
            if (ammo > 0)
            {
                GameObject instantiatedProjectile = Instantiate(shooterBaseConfig.ProjectilePrefab, transform.position + Vector3.forward*shooterConfig.DistanceFromShipOrigin, transform.rotation);
                instantiatedProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shooterBaseConfig.BulletSpeed, ForceMode.Impulse);
                instantiatedProjectile.GetComponent<Projectile>().SetOwner(GetComponentInParent<IShooter>());
                Destroy(instantiatedProjectile, shooterBaseConfig.LifeTime);
                ammo--;
            }
        }

        public void SetFireDirection(Vector3 _direction)
        {
            if (_direction != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(_direction.normalized);
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
        public float DistanceFromShipOrigin;
        public int AddedAmmo;
        public int MaxAmmo;
    }
}
