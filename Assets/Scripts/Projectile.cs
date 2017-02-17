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
        GameObject ownerObj = Owner.GetOwner();
        if (!other.gameObject.Equals(ownerObj))
        {
            IDamageable objectToDamage = other.gameObject.GetComponent<IDamageable>();
            if (objectToDamage != null)
            {
                foreach (IDamageable item in Owner.GetDamageable())
                {
                    if (item.GetType() == objectToDamage.GetType())
                    {
                        ownerObj.GetComponent<Player>().Points = objectToDamage.Damage(Damage);
                        Destroy(gameObject);        
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
