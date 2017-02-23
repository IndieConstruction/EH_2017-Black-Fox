using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    IShooter shooter;
    float startTime;
    float timeToCount = 5f;
    float Damage = 1;
    Agent agent;

    void Start()
    {
        agent = shooter.GetOwner();            //Salva all'interno di ownerObj l'Owner (cioé colui che l'ha sparato)
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
        if (!other.gameObject.Equals(agent))
        {
            // Controlla se l'oggetto con cui ha colliso ha l'interfaccia IDamageable e salva un riferimento di tale interfaccia
            IKillable canCollecte = other.GetComponent<IKillable>();
            IDamageable damageables = other.gameObject.GetComponent<IDamageable>();                 
            if (damageables != null)                                                                
            {
                
                //Controlla se all'interno della lista di oggetti Danneggiabili, contenuta da Owner (chi ha sparato il proiettile)
                foreach (IDamageable item in shooter.GetDamageable())
                {
                    // E' presente l'oggetto con cui il proiettile è entrato in collisione.
                    if (item.GetType() == damageables.GetType())
                    {
                        if (canCollecte != null)
                        {
                            canCollecte.CheckIfKillable(agent.playerIndex);
                        }

                        damageables.Damage(Damage);         // Se è un oggetto che può danneggiare, richiama la funzione che lo danneggia e se lo distrugge assegna i punti dell'uccisione all'agente che lo ha ucciso
                        
                       
                        Destroy(gameObject);                //Distrugge il proiettile
                        break;                              // Ed esce dal foreach.
                    }
                }
            }
        }
    }

    //Setta chi è il proprietario del proiettile, cioé chi lo spara.
    public void SetOwner(IShooter _owner)
    {
        shooter = _owner;
    }
}
