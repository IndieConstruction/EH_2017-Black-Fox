using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    IShooter owner;
    float startTime;
    float timeToCount = 5f;
    float damage = 1;

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
        //Se il gameobject con cui è entrato in collisione è diverso da quello che lo ha sparato, allora entra nell'if.
        if (other.gameObject.GetComponent<IShooter>() != null)
            //TODO: Il Proiettile ignora tutti gli oggetti tranne coloro che hanno un IShooter.
            if (owner.GetOwner() == other.gameObject.GetComponent<IShooter>().GetOwner())
                return;

        // Controlla se l'oggetto con cui ha colliso ha l'interfaccia IDamageable e salva un riferimento di tale interfaccia
            
        IDamageable damageables = other.gameObject.GetComponent<IDamageable>();                 
        if (damageables != null)                                                                
        {
                
            //Controlla se all'interno della lista di oggetti Danneggiabili, contenuta da Owner (chi ha sparato il proiettile)
            foreach (IDamageable item in owner.GetDamageable())
            {
                // E' presente l'oggetto con cui il proiettile è entrato in collisione.
                if (item.GetType() == damageables.GetType())
                {  
                    damageables.Damage(damage, owner.GetOwner());         // Se è un oggetto che può danneggiare, richiama la funzione che lo danneggia e se lo distrugge assegna i punti dell'uccisione all'agente che lo ha ucciso     
                    Destroy(gameObject);                //Distrugge il proiettile
                    break;                              // Ed esce dal foreach.
                }
            }
        }
        
    }

    //Setta chi è il proprietario del proiettile, cioé chi lo spara.
    public void SetOwner(IShooter _owner)
    {
        owner = _owner;
    }
}
