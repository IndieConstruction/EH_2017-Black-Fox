using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlackFox
{

    public class CleanSweep : PowerUpBase
    {
        public Cleaner cleaner;
		ParticleSystem cleanerParticle;
        
        void Start()
        {
            DestroyAfterUse = false;
			cleanerParticle = GetComponentInChildren<ParticleSystem> ();
        }

        public override void UsePowerUp()
        {
            LifeTime = 5;
            GetComponent<MeshRenderer>().enabled = false;
			if (cleanerParticle != null)
				cleanerParticle.Play ();
            cleaner.transform.DOScale(10f, 2f).OnComplete(() => { cleaner.transform.DOScale(0f, 0.8f).SetDelay(LifeTime - 1f); });
        }
    }
}