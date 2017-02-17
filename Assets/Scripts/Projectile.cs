﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    IShooter Owner;
    float startTime;
    float timeToCount = 5f;
    float Damage = 1;
    GameObject ownerObj;

    void Start()
    {
        ownerObj = Owner.GetOwner();
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
        if (!other.gameObject.Equals(ownerObj))
        {
            IDamageable damageables = other.gameObject.GetComponent<IDamageable>();
            if (damageables != null)
            {
                foreach (IDamageable item in Owner.GetDamageable())
                {
                    if (item.GetType() == damageables.GetType())
                    {
                        ownerObj.GetComponent<Player>().Points += damageables.Damage(Damage);
                        Destroy(gameObject);
                        break;       
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