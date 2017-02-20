using System.Collections;
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

    /// <summary>
    /// Rotate the object on the Y axis following the left stick position.
    /// </summary>
    /// <param name="_rotationSpeed">the speed of rotation (Positive to turn left, Negative to tirn right) </param>
    public void RotationTowards(Vector3 axis)
    {
        //Rotazione in base allo schermo -- NON CANCELLARE !
        Vector3 newDir = Vector3.RotateTowards(transform.forward, axis, Time.deltaTime * RotationSpeed, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void Rotation(float _direction)
    {
        //rotazione in bsase all'agente
        transform.Rotate(Vector3.up * RotationSpeed * _direction * Time.deltaTime);
    }


    /// <summary>
    /// test to stop the rotation of the object if it collides another object.
    /// </summary>
    void StopRotation()
    {
        //Not much time to save 2 different values in the variables.
        float temp1 = transform.rotation.y;
        float temp2 = transform.rotation.y;

        if (!Input.anyKey && temp1 != temp2)
        {
            Debug.Log("Stai avvitando");
        }
    }
}
