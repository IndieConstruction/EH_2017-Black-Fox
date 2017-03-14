using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class Core : MonoBehaviour, IDamageable
    {

        float life;
        public float MaxLife = 10;              // La vita massima che può avere il Core e che viene impostata al riavvio di un round perso
        GameManager gameManager;

        private void Start()
        {
            life = MaxLife;
        }

        #region Interfacce

        public void Damage(float _damage, GameObject _attacker)
        {
            life -= _damage;
            if (life < 1)
            {
                Destroy(gameObject);
            }
        }

        #endregion
    }
}