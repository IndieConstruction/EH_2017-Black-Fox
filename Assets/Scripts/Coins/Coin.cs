using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class Coin : MonoBehaviour, IPoollableObject {

        public PoolManager poolManager { get; set; }

        public GameObject GameObject { get { return gameObject; } }

        public bool IsActive { get; set; }

        public void Activate()
        {
            IsActive = true;
            GetComponent<MeshCollider>().enabled = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }



        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Ship>() != null)
            {

            }
        }
    }
}