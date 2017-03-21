using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class Core : MonoBehaviour, IDamageable
    {

        float life;
        public float MaxLife = 10;      // La vita massima che può avere il Core e che viene impostata al riavvio di un round perso

        private void Start()
        {
            life = MaxLife;
        }

        private void OnEnable()
        {
            EventManager.OnLevelInit += HandleOnLevelInit;
        }

        private void OnDisable()
        {
            EventManager.OnLevelInit -= HandleOnLevelInit;
        }

        #region Event Handler
        void HandleOnLevelInit()
        {
            if (life == 0)
                life = MaxLife;

            EnableComponents(true);
        }
        #endregion

        void EnableComponents(bool _value)
        {
            GetComponentInChildren<MeshRenderer>().enabled = _value;
            GetComponent<Collider>().enabled = _value;
        }

        #region Interfacce

        public void Damage(float _damage, GameObject _attacker)
        {
            life -= _damage;
            if (life < 1)
            {
                LevelManager.OnCoreDeath();
                EnableComponents(false);
            }
        }

        #endregion
    }
}