using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeForcedLook : MonoBehaviour {

    public GameObject Target;

    private void FixedUpdate()
    {
        if(Target != null)
        transform.LookAt(Target.transform);
    }
}
