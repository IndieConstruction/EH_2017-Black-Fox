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
        Vector3 normal = Vector3.up;
        Vector3 proj;
        Quaternion targetRotation;
        Quaternion deltaRotation;
        Vector3 deltaAngles;
        Vector3 worldDeltaAngles;
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

            if(_target != Vector3.zero)
            Yaw(_target, Vector3.up);
        }
        #endregion
        
        void Yaw(Vector3 _target, Vector3 _normal)
        {
            normal = _normal;
            // Compute target rotation (align rigidybody's up direction to the normal vector)

            proj = Vector3.ProjectOnPlane(_target, normal);
            targetRotation = Quaternion.LookRotation(proj, normal); // The target rotation can be replaced with whatever rotation you want to align to

            deltaRotation = Quaternion.Inverse(transform.rotation) * targetRotation;
            deltaAngles = GetRelativeAngles(deltaRotation.eulerAngles);
            worldDeltaAngles = transform.TransformDirection(deltaAngles);

            // alignmentSpeed controls how fast you rotate the body towards the target rotation
            // alignmentDamping prevents overshooting the target rotation
            // Values used: alignmentSpeed = 0.025, alignmentDamping = 0.2
            rigid.AddTorque(MovementConfig.RotationSpeed * worldDeltaAngles - MovementConfig.RotationSpeed *10 * rigid.angularVelocity, ForceMode.Force);
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
