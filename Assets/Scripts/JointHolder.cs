using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointHolder : MonoBehaviour {

    public float Distance;
    public Transform Target;
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position,Target.position) > Distance)
        {
            Target.position = transform.position - Vector3.one;
        }
	}
}
