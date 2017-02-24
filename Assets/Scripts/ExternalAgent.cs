using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalAgent : MonoBehaviour, IDamageable {

    Transform target;
    public float life = 10;
    public float velocity = 6000;
    public float damage = 1;

    List<IDamageable> damageablesList;

    public float Life
    {
        get { return life; }
        set { life = value; }
    }

    private void Update()
    {
        MoveTowards();
    }

    void MoveTowards()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * velocity, ForceMode.Force);
    }

    public void Initialize(Transform _target, List<IDamageable> _damageables)
    {
        target = _target;
        damageablesList = _damageables;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Controlla se l'oggetto con cui ha colliso ha l'interfaccia IDamageable e salva un riferimento di tale interfaccia
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            //Controlla se all'interno della lista di oggetti Danneggiabili, passata da SpawnExternalAgent
            foreach (IDamageable item in damageablesList)
            {
                // E' presente l'oggetto con cui l'agente esterno è entrato in collisione.
                if (item.GetType() == damageable.GetType())
                {
                    damageable.Damage(damage, null);        // Se è un oggetto che può danneggiare, richiama la funzione che lo danneggia
                    Destroy(gameObject);                    //Distrugge l'agente esterno
                    break;                                  // Ed esce dal foreach.
                }
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
            GetComponent<Collider>().isTrigger = false;
    }

    #region Interface

    public void Damage(float _damage, GameObject _attacker)
    {
        Life -= _damage;
        if(Life < 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}

