using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class CleanSweep : PowerUpBase
    {
        public Cleaner Cleaner;

        
        void Start()
        {
            DestroyAfterUse = false;
        }

        public override void UsePowerUp()
        {
            LifeTime = 5;
            Cleaner.GetComponent<SphereCollider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}