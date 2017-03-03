using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

    public float velocity;


    void Start ()
    {
		
	}
	

	void FixedUpdate ()
    {
        MoveForward();
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * velocity);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Wall")
        {
            Debug.Log("kill");
            Destroy(gameObject);
        }
            
    }
}
