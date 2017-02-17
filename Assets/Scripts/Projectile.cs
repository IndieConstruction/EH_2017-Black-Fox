using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    IShooter Owner;
    float startTime;
    float timeToCount = 5f;
    float Damage = 1;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (Time.time >= startTime + timeToCount)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.Equals(Owner.GetOwner()))
        {
            IDamageable damageables = other.gameObject.GetComponent<IDamageable>();
            if (damageables != null)
            {
                foreach (IDamageable item in Owner.GetDamageable())
                {
                    if (item.GetType() == damageables.GetType())
                    { 
                        Destroy(gameObject);
                        damageables.Damage(Damage);              
                    }
                }
            }
        }
    }

    public void SetOwner(IShooter _owner)
    {
        Owner = _owner;
    }
}
