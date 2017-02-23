using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalAgent : MonoBehaviour, IDamageable {

    Transform target;
    public float life;
    public float velocity;

    public float Life
    {
        get { return life; }
        set { life = value; }
    }
	void Update ()
    {
        MoveTowards();
    }

    void MoveTowards()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * velocity, ForceMode.Force);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
            GetComponent<Collider>().isTrigger = false;
    }

    #region Interface

    public void Damage(float _damage)
    {
        Life -= _damage;
        if (Life < 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
