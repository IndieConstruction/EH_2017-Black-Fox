using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class BlackHole : MonoBehaviour
    {

        public float Range;

        // la velocità con cui viene spostato verso il centro
        public float Attraction = 15;

        private void OnTriggerStay(Collider other)
        {
            GameObject tempObj = other.gameObject;
            
            

            if (tempObj.GetComponent<Agent>() != null)
            {
                tempObj.transform.position = Vector3.MoveTowards(tempObj.transform.position, transform.localPosition, Attraction);
                if (Vector3.Distance(tempObj.transform.position, transform.position) <= Range)
                {
                    Destroy(tempObj);
                }
                
                /*Vector3 Distance = tempObj.transform.position - transform.position;
                Distance.z = 0;

                float SquareOfDistance = Distance.sqrMagnitude;

                if(SquareOfDistance > 0.0001f)
                {
                    //tempObj.GetComponent<Rigidbody>().AddForce(new Vector3(transform.position.x, transform.position.y, transform.position.z), ForceMode.Acceleration);
                }*/
            }

        }
    }
}