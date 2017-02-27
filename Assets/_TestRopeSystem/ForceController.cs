using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceController : MonoBehaviour {

    public float force = 3;
    Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    private void FixedUpdate() {
        if (Input.GetKey(KeyCode.A)) {
            rigidBody.AddForce(Vector3.left * force, ForceMode.Force);
            Debug.Log("Left");
        }
        if (Input.GetKey(KeyCode.W)) {
            rigidBody.AddForce(Vector3.up * force, ForceMode.Force);
            Debug.Log("Up");
        }
        if (Input.GetKey(KeyCode.D)) {
            rigidBody.AddForce(Vector3.right * force, ForceMode.Force);
            Debug.Log("Right");
        }
        if (Input.GetKey(KeyCode.S)) {
            rigidBody.AddForce(Vector3.down * force, ForceMode.Force);
            Debug.Log("Down");
        }
    }
}
