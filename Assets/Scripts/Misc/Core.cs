using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BlackFox
{
    public class Core : MonoBehaviour, IDamageable
    {

        float life;
        public float MaxLife = 10;      // La vita massima che può avere il Core e che viene impostata al riavvio di un round perso

        Image Ring;

        public void Setup()
        {
            Ring = GetComponentInChildren<Image>();
            life = MaxLife;
            OnDataChange();
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

        #region API
        public void Init()
        {
            transform.DOScale(Vector3.one, 0.1f);
            if (life <= 0)
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
            transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.5f);
            if (life < 1)
            {
                GameManager.Instance.LevelMng.PoolMng.GetPooledObject(transform.position);
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { GameManager.Instance.LevelMng.CoreDeath(); });
            }
        }

        #endregion
    }
}