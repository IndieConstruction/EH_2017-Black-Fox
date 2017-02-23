using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Shooter : MonoBehaviour {

    public GameObject projectile;
    public Agent owner;
    public float LifeTime = 10;
    public float bulletSpeed;
    

    /// <summary>
    /// Spara un proiettile
    /// </summary>
    public void ShootBullet(Agent _owner)
    {
        GameObject instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation);
        instantiatedProjectile.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * bulletSpeed, ForceMode.Impulse);
        instantiatedProjectile.GetComponent<Projectile>().SetOwner(owner);
        Destroy(instantiatedProjectile, LifeTime);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ShootBullet(owner);
        }
    }
}