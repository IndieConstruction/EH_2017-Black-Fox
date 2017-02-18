using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject projectile;
    public Transform bulletspawn;
    public float bulletSpeed;

    /// <summary>
    /// Spara un proiettile
    /// </summary>
    public void ShootBullet()
    {
        GameObject instantiatedProjectile = Instantiate(projectile, bulletspawn.position, bulletspawn.rotation);
        instantiatedProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        instantiatedProjectile.GetComponent<Projectile>().SetOwner(GetComponentInParent<IShooter>());
    }
}