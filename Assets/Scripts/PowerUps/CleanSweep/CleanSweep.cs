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
        
        void Start()
        {
            DestroyAfterUse = false;
        }

        public override void UsePowerUp()
        {
            LifeTime = 5;
            GetComponent<MeshRenderer>().enabled = false;
            cleaner.transform.DOScale(10f, 2f).OnComplete(() => { cleaner.transform.DOScale(0f, 0.8f).SetDelay(LifeTime - 1f); });
        }
    }
}