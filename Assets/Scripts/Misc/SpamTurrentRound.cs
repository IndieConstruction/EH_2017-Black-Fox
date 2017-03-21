using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamTurrentRound : MonoBehaviour
{





    //public float TurrentSpam;
    public GameObject turrent;

    // Instantiate the prefab somewhere between -10.0 and 10.0 on the x-z plane 
    void Start()
    {
        Vector3 position = new Vector3(Random.Range(-70.0f, 100.0f), 0, Random.Range(-100.0f, 100.0f));
        Instantiate(turrent, position, Quaternion.identity);
       


    }


    



}
