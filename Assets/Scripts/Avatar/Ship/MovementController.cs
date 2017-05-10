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
        #region Rotation fields
        Vector3 appliedTroque;
        //yaw fileds
        Vector3 proj;
        Quaternion targetRotation;
        Quaternion deltaRotation;
        Vector3 deltaAngles;
        Vector3 worldDeltaAngles;
        //roll fields
        Vector3 rollProj;
        Quaternion rollTargetRotation;
        Quaternion rollDeltaRotation;
        Vector3 rollDeltaAngles;
        Vector3 rollWorldDeltaAngles;
        #endregion

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

            if (_target != Vector3.zero)
            {
                appliedTroque = GetYawTroque(_target, Vector3.up);// + GetRollTroque(rigid.velocity, transform.forward);
                rigid.AddTorque(appliedTroque, ForceMode.Force);
            }
                
        }
        #endregion
        Vector3 GetRollTroque( Vector3 _target, Vector3 _normal)
        {
            // Compute target rotation (align rigidybody's up direction to the normal vector)

            rollProj = Vector3.ProjectOnPlane(_target, _normal);
            rollTargetRotation = Quaternion.FromToRotation(transform.up, rollProj);

            rollDeltaRotation = Quaternion.Inverse(transform.rotation) * rollTargetRotation;
            rollDeltaAngles = GetRelativeAngles(rollDeltaRotation.eulerAngles);
            rollWorldDeltaAngles = transform.TransformDirection(rollDeltaAngles);

            return MovementConfig.RotationSpeed * rollWorldDeltaAngles - MovementConfig.RotationSpeed * 10 * rigid.angularVelocity;
        }
        Vector3 GetYawTroque(Vector3 _target, Vector3 _normal)
        {
            // Compute target rotation (align rigidybody's up direction to the normal vector)

            proj = Vector3.ProjectOnPlane(_target, _normal);
            targetRotation = Quaternion.LookRotation(proj, _normal);

            deltaRotation = Quaternion.Inverse(transform.rotation) * targetRotation;
            deltaAngles = GetRelativeAngles(deltaRotation.eulerAngles);
            worldDeltaAngles = transform.TransformDirection(deltaAngles);

            // reference value: angular velocity more or less 10 times larger than worldDelta
            return MovementConfig.RotationSpeed * worldDeltaAngles - MovementConfig.RotationSpeed * 10 * rigid.angularVelocity;
        }

        // Convert angles above 180 degrees into negative/relative angles
        Vector3 GetRelativeAngles(Vector3 angles)
        {
            Vector3 relativeAngles = angles;
            if (relativeAngles.x > 180f)
                relativeAngles.x -= 360f;
            if (relativeAngles.y > 180f)
                relativeAngles.y -= 360f;
            if (relativeAngles.z > 180f)
                relativeAngles.z -= 360f;

            return relativeAngles;
        }
    }

    [Serializable]
    public class MovementControllerConfig
    {
        public float RotationSpeed;
        public float MovmentSpeed;
    }
}
