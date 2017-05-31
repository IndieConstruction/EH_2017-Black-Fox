using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlackFox
{
    public class ExternalAgent : MonoBehaviour, IDamageable
    {
        Transform target;
        public float life = 10;
        public float velocity = 5;
        public float damage = 1;
        AlertIndicator alertIndicator;

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
            GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * velocity, ForceMode.Acceleration);
        }

        public void Initialize(Transform _target, List<IDamageable> _damageables)
        {
            target = _target;
            damageablesList = _damageables;
            alertIndicator = GetComponent<AlertIndicator>();
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
                        GameManager.Instance.LevelMng.ExplosionPoolMng.GetPooledObject(transform.position);
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
            transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
            if (Life < 1)
            {
                GameManager.Instance.LevelMng.ExplosionPoolMng.GetPooledObject(transform.position);
                GameManager.Instance.CoinMng.CoinController.InstantiateCoin(transform.position);
                GetComponent<Collider>().enabled = false;
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { Destroy(gameObject); });
            }
        }

        #endregion
    }
}

