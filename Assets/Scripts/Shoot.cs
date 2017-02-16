using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public Rigidbody projectile;
    public Transform bulletspawn;
    public float bulletSpeed;    

    public void ShootBullet()
    {
        Rigidbody instantiatedProjectile = Instantiate(projectile, bulletspawn.position, bulletspawn.rotation);
        instantiatedProjectile.AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        Destroy(instantiatedProjectile.gameObject, 5f);
    }
}
