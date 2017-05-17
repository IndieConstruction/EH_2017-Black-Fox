﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class DamagingShell : MonoBehaviour {

        List<IDamageable> _damageables = new List<IDamageable>();
        float duration = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_time">la durata del power up</param>
        public void Init(float _time)
        {
            _damageables = GetComponent<Ship>().GetDamageable();
            duration = _time;
        }

        private void Update()
        {
            if(duration > 0) { 
                duration -= Time.deltaTime;
                 if (duration <= 0)
                    Destroy(this); 
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            IDamageable collidingDamageable = collision.gameObject.GetComponent<IDamageable>();
            if (collidingDamageable != null)
            {
                foreach (IDamageable damageable in _damageables)
                {
                    if (damageable.GetType() == collidingDamageable.GetType())
                        collidingDamageable.Damage(1, gameObject);
                }
            }
        }
    }
}