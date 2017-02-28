using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MovementForRopes : MovementController {

    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update () {
        rigid.AddRelativeForce(Vector3.forward * Input.GetAxis("Vertical") * MovmentSpeed, ForceMode.Force);
        rigid.AddRelativeTorque(Vector3.up * RotationSpeed * Input.GetAxis("Horizontal") * Time.deltaTime, ForceMode.Acceleration);
    }
}
