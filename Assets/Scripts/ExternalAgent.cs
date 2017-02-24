using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ExternalAgent : MonoBehaviour, IDamageable {

    Transform target;
    public float life;
    public float velocity;
    public float damage;

    List<IDamageable> damageables;

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
        damageables = _damageables;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto con cui ha colliso ha l'interfaccia IDamageable e salva un riferimento di tale interfaccia
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            //Controlla se all'interno della lista di oggetti Danneggiabili, contenuta da Owner (chi ha sparato il proiettile)
            foreach (IDamageable item in damageables)
            {
                // E' presente l'oggetto con cui il proiettile è entrato in collisione.
                if (item.GetType() == damageable.GetType())
                {
                    damageable.Damage(damage, null);        // Se è un oggetto che può danneggiare, richiama la funzione che lo danneggia e se lo distrugge assegna i punti dell'uccisione all'agente che lo ha ucciso     
                    Destroy(gameObject);                    //Distrugge il proiettile
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

