using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class Core : MonoBehaviour, IDamageable
    {

        float life;
        public float MaxLife = 10;      // La vita massima che può avere il Core e che viene impostata al riavvio di un round perso

        Image Ring;

        private void Start()
        {
            Ring = GetComponentInChildren<Image>();
            life = MaxLife;
            OnDataChange();
        }

        private void OnEnable()
        {
            EventManager.OnLevelInit += HandleOnLevelInit;
        }

        private void OnDisable()
        {
            EventManager.OnLevelInit -= HandleOnLevelInit;
        }

        void OnDataChange()
        {
            Ring.fillAmount = life / MaxLife;

            if (Ring.fillAmount < 0.3f)
            {
                Ring.color = Color.red;
            }
            else if (Ring.fillAmount > 0.7f)
            {
                Ring.color = Color.green;
            }
            else
            {
                Ring.color = Color.yellow;
            }
        }

        #region Event Handler
        void HandleOnLevelInit()
        {
            if (life == 0)
            {
                life = MaxLife;
                OnDataChange();
            }
        }
        #endregion

        #region Interfacce

        public void Damage(float _damage, GameObject _attacker)
        {
            life -= _damage;
            OnDataChange();
            if (life < 1)
            {
                EventManager.OnCoreDeath();
            }
        }

        #endregion
    }
}