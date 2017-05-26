using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class ExplosionPoolObj : MonoBehaviour, IPoollableObject
    {

        float timeToKill = 3;

        public ParticleSystem ExplosionParticles;

        public PoolManager poolManager { get; set; }

        public GameObject GameObject { get { return gameObject; } }

        public bool IsActive { get; set; }

        public void Activate()
        {
            IsActive = true;
            if (ExplosionParticles.isPlaying)
                ExplosionParticles.Stop();
            ExplosionParticles.Play();
        }

        public void Deactivate()
        {
            IsActive = false;
            poolManager.ReleasedPooledObject(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsActive)
                return;
            timeToKill -= Time.deltaTime;
            if (timeToKill < 0)
            {
                Deactivate();
                timeToKill = 1;
            }
        }
    }
}