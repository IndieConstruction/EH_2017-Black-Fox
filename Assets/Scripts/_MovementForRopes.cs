using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MovementForRopes : MovementController {

    Rigidbody Rigid;

    private void Start()
    {
        Rigid = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update () {
        Rigid.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * MovmentSpeed, ForceMode.Force);
        Rigid.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, ForceMode.Acceleration);
    }
}
