﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public float RotationSpeed;
    public float MovmentSpeed;
    Rigidbody Rigid;

    // Use this for initialization
    void Start () {
        Rigid = GetComponent<Rigidbody>();
	}
	
    /// <summary>
    /// Add a relative force to the rigidbody of the object.
    /// </summary>
    /// <param name="_Speed">the speed that the object must to have</param>
    public void Movement(float _Speed)
    {
        Rigid.AddRelativeForce(Vector3.forward * _Speed * MovmentSpeed, ForceMode.Acceleration);
    }

    public void Rotation(float _direction)
    {
        //rotazione in base all'agente
        Rigid.AddRelativeTorque(Vector3.up * RotationSpeed * _direction * Time.deltaTime,ForceMode.Acceleration);
    }
}
