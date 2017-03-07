using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class Arrow : MonoBehaviour
    {

        public float Force = 500000;
        public int IDArrow;



        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Agent>() != null)
            {
                other.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Acceleration);
            }
        }
    }
}
