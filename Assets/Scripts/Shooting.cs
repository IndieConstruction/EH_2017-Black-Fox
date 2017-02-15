using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public Rigidbody projectile;
    public Transform bulletspawn;
    public int Velocity=30;

        void Start()
    {
        Cursor.visible = false;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot(Velocity);
        }

    }

    public void Shoot(int _speed) {
        Rigidbody instantiatedProjectile = Instantiate(projectile, bulletspawn.position, bulletspawn.rotation);
        instantiatedProjectile.AddRelativeForce(Vector3.forward * _speed, ForceMode.Impulse);
        Destroy(instantiatedProjectile.gameObject, 2f);
    }
}
