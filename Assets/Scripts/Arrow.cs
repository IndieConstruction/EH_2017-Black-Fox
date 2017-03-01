using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float Force = 500000;


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Agent>() != null)
        {
            Debug.Log("collisione");
            other.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);
            
        }
    }
}
