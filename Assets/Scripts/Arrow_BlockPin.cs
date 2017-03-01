using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow_BlockPin : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Agent>() != null)
            other.GetComponent<PlacePin>().CanPlace = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Agent>() != null)
            other.GetComponent<PlacePin>().CanPlace = true;
    }
}
