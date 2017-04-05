using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class BlockPin : MonoBehaviour
    {

        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<Avatar>() != null)
                other.GetComponent<PlacePin>().CanPlace = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Avatar>() != null)
                other.GetComponent<PlacePin>().CanPlace = true;
        }
    }
}
