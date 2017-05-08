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
        /// Self orient and propel toward _target
        /// </summary>
        /// <param name="_target">Provide direction of acceleration and magnitude of the pulse</param>
        public void Move(Vector3 _target)
        {
            rigid.AddForce(_target * MovementConfig.MovmentSpeed, ForceMode.Force);
            if(_target != Vector3.zero)
                rigid.MoveRotation(Quaternion.Slerp(rigid.rotation,Quaternion.LookRotation(_target),Time.deltaTime * MovementConfig.RotationSpeed));
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
