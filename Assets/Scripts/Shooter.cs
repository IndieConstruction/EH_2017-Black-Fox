using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Shooter : MonoBehaviour {

    public GameObject projectile;
    public float LifeTime = 10f;
    public float bulletSpeed = 15000f;
    public int AddedAmmo = 10;
    public int MaxAmmo = 50;
    public int ammo = 0;

    /// <summary>
    /// Spara un proiettile
    /// </summary>
    public void ShootBullet()
    {
        if (ammo > 0)
        {
            GameObject instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation);
            instantiatedProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
            instantiatedProjectile.GetComponent<Projectile>().SetOwner(GetComponentInParent<IShooter>());
            Destroy(instantiatedProjectile, LifeTime);
            ammo--;//ammo =ammo -1
        }
    }

    public void AddAmmo()
    {
        if (ammo < MaxAmmo)
            ammo += AddedAmmo;  //ammo=ammo+AddedAmmo; 
        else if (ammo > MaxAmmo)
            ammo = AddedAmmo;
    }
}