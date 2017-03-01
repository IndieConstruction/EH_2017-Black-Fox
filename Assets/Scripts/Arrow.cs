using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    public float Force = 100000;


    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Agent>() != null)
        {
            Debug.Log("collisione");
            other.GetComponent<Rigidbody>().AddForce(transform.forward * Force, ForceMode.Impulse);
            other.GetComponent<PlacePin>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Agent>() != null)
        {
            other.GetComponent<PlacePin>().enabled = true;
        }
    }
}
