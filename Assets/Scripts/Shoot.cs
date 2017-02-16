using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public Rigidbody projectile;
    public Transform bulletspawn;

    public void ShootBullet(int _speed)
    {
        Rigidbody instantiatedProjectile = Instantiate(projectile, bulletspawn.position, bulletspawn.rotation);
        instantiatedProjectile.AddRelativeForce(Vector3.forward * _speed, ForceMode.Impulse);
        Destroy(instantiatedProjectile.gameObject, 2f);
    }
}
