using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OstacoloMobile : MonoBehaviour {

    public float Speed = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        MoveToWord();
	}

    void MoveToWord()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * Speed, ForceMode.Force);
    }

}