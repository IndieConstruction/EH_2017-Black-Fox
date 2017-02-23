using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ExternalAgent : MonoBehaviour, IDamageable {

    Transform target;
    public float life;
    public float velocity;
    public float Damage;

    List<IDamageable> Damageables;                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable

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

    public void Initialize(Transform _target, List<IDamageable> _damageables)
    {
        target = _target;
        Damageables = _damageables;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Controlla se l'oggetto con cui ha colliso ha l'interfaccia IDamageable e salva un riferimento di tale interfaccia
        IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            //Controlla se all'interno della lista di oggetti Danneggiabili, contenuta da Owner (chi ha sparato il proiettile)
            foreach (IDamageable item in Damageables)
            {
                // E' presente l'oggetto con cui il proiettile è entrato in collisione.
                if (item.GetType() == damageable.GetType())
                {
                    //damageable.Damage(Damage, null);        // Se è un oggetto che può danneggiare, richiama la funzione che lo danneggia e se lo distrugge assegna i punti dell'uccisione all'agente che lo ha ucciso     
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

    public void Damage(float _damage, PlayerIndex _attacker)
    {
        Life -= _damage;
        if (Life < 1)
        {
            Destroy(gameObject);
        }
    }

    #endregion
}
