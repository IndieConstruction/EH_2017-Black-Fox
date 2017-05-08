using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour
    {
        MovementControllerConfig MovementConfig
        {
            get { return ship.avatar.AvatarData.shipConfig.movementConfig; }
        }

        Ship ship;
        Rigidbody rigid;

        #region API
        public void Init(Ship _ship, Rigidbody _rigid)
        {
            ship = _ship;
            rigid = _rigid;
        }

        /// <summary>
        /// Add a relative force to the rigidbody of the object.
        /// </summary>
        /// <param name="_axisValue">the speed that the object must to have</param>
        public void Movement(float _axisValue)
        {
            rigid.AddRelativeForce(Vector3.forward * _axisValue * MovementConfig.MovmentSpeed, ForceMode.Force);
        }

        public void Rotation(float _axisValue)
        {
            //rotazione in base all'agente
            rigid.MoveRotation(rigid.rotation * Quaternion.Euler(Vector3.up * MovementConfig.RotationSpeed * _axisValue * Time.deltaTime));
        }
        #endregion
    }

    [Serializable]
    public class MovementControllerConfig
    {
        public float RotationSpeed;
        public float MovmentSpeed;
    }
}
