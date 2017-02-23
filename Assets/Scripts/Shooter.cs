using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Shooter : MonoBehaviour {

    public GameObject projectile;
    public float LifeTime = 10;
    public float bulletSpeed;
    

    /// <summary>
    /// Spara un proiettile
    /// </summary>
    public void ShootBullet()
    {
        GameObject instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation);
        instantiatedProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        instantiatedProjectile.GetComponent<Projectile>().SetOwner(GetComponentInParent<IShooter>());
        Destroy(instantiatedProjectile, LifeTime);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S))
        {
            //ShootBullet();
        }
    }
}