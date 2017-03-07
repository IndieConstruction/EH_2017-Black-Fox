using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour
    {

        public float RotationSpeed;
        public float MovmentSpeed;
        Rigidbody Rigid;

        // Use this for initialization
        void Start()
        {
            Rigid = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Add a relative force to the rigidbody of the object.
        /// </summary>
        /// <param name="_axisValue">the speed that the object must to have</param>
        public void Movement(float _axisValue)
        {
            Rigid.AddRelativeForce(Vector3.forward * _axisValue * MovmentSpeed, ForceMode.Force);
        }

        public void Rotation(float _axisValue)
        {
            //rotazione in base all'agente
            Rigid.AddRelativeTorque(Vector3.up * RotationSpeed * _axisValue * Time.deltaTime, ForceMode.Force);
        }
    }
}
