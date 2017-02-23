using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointHolder : MonoBehaviour {

    float distance;
    Vector3 target;
    float step;
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(transform.position,target) > distance)
        {
            Vector3.MoveTowards(transform.position, target, step);
        }
	}

    public void Init(Vector3 _target, float _distance, float _step)
    {
        distance = _distance;
        target = _target;
        step = _step;
    }
}
