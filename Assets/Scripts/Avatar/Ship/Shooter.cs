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
                    shooterBaseConfig = ship.Avatar.AvatarData.shipConfig.shooterConfig.ShooterBaseConfig;
                return ship.Avatar.AvatarData.shipConfig.shooterConfig;
            }
        }

        Ship ship;
        Material bulletMaterial;

        int ammo;
        public int Ammo
        {
            get { return ammo; }
            set { ammo = value;
                ship.Avatar.OnAmmoUpdate();
            }
        }

        #region API
        public void Init(Ship _ship)
        {
            ship = _ship;
        }

        public override void ShootBullet()
        {
            if (Ammo > 0)
            {
                GameObject instantiatedProjectile = Instantiate(shooterBaseConfig.ProjectilePrefab, transform.position + transform.forward*shooterConfig.DistanceFromShipOrigin, transform.rotation);
                instantiatedProjectile.GetComponentInChildren<MeshRenderer>().material = ship.Avatar.AvatarData.ColorSets[ship.Avatar.ColorSetIndex].PinMaterial;
                instantiatedProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * shooterBaseConfig.BulletSpeed, ForceMode.Impulse);
                instantiatedProjectile.GetComponent<Projectile>().Init(GetComponentInParent<IShooter>());
                Destroy(instantiatedProjectile, shooterBaseConfig.LifeTime);
                Ammo--;
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
        public float ProjectileDamage;
        public int AddedAmmo;
        public int MaxAmmo;
    }
}
