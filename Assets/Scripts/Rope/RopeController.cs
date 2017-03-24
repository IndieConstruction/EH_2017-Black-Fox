using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Rope
{
    [RequireComponent (typeof(ConfigurableJoint))]
    public class RopeController : MonoBehaviour
    {
        public GameObject FragmentPrefab;
        public Transform TailAnchorPoint;
        public Transform HeadAnchorPoint;
        public int MaxLength = 80;
        [Range(.0f, 1f)]
        public float DensityOfFragments = .1f;

        List<GameObject> fragments = new List<GameObject>();
        List<Vector3> rendPosition = new List<Vector3>();
        float ropeWidth;
        Vector3 offSet;

        float fragmentDistance;

        private void Awake()
        {
            transform.position = HeadAnchorPoint.position;
            GetComponent<ConfigurableJoint>().connectedBody = HeadAnchorPoint.GetComponent<Rigidbody>();
        }
        void Start()
        {
            ropeWidth = GetComponent<LineRenderer>().widthMultiplier;
            fragments.Add(gameObject);
            BuildRope(gameObject);
        }       

        /// <summary>
        /// Extend the rope toward the AnchorPoint
        /// </summary>
        /// <param name="_lastPiece">Current end of the rope</param>
        void BuildRope(GameObject _lastPiece)
        {
            Vector3 position;
            CapsuleCollider collider;
            ConfigurableJoint joint;

            //Relative position of newPieces to previouses
            offSet = GetOffSet(_lastPiece.transform);

            //Keep building the rope until the AnchorPoint ore the MaxLength are reached
            for (int i = fragments.Count; i < MaxLength; i++)
            {
                //Add a new Fragment to the rope
                position = fragments[i - 1].transform.position + offSet;
                GameObject newFragment = Instantiate(FragmentPrefab, position, Quaternion.LookRotation(position));
                fragments.Add(newFragment);
                newFragment.transform.parent = fragments[0].transform;
                newFragment.name = "Fragment_" + i;
                //Collider configuration
                collider = newFragment.GetComponentInChildren<CapsuleCollider>();
                collider.radius = ropeWidth / 2;
                collider.center = Vector3.forward * fragmentDistance / 2;
                collider.height = fragmentDistance + collider.radius;
                collider.GetComponent<RopeForcedLook>().Target = fragments[i - 1];
                //Joint Configuration
                joint = newFragment.GetComponent<ConfigurableJoint>();
                joint.connectedBody = fragments[i - 1].GetComponent<Rigidbody>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = offSet;
                //Is the AnchroPoint closer than the offSet? If so stop building the rope
                if (Vector3.Distance(position, TailAnchorPoint.position) <= fragmentDistance)
                {
                    joint = TailAnchorPoint.GetComponent<ConfigurableJoint>();
                    joint.connectedBody = newFragment.GetComponent<Rigidbody>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = offSet;
                    break;
                }
            }

            if (TailAnchorPoint.GetComponent<ConfigurableJoint>().connectedBody == null)
            {
                TailAnchorPoint.GetComponent<ConfigurableJoint>().connectedBody = fragments[fragments.Count - 1].GetComponent<Rigidbody>();
                Debug.Log("WARNING: MaxLength not enough to reach " + TailAnchorPoint.name + " in " + TailAnchorPoint.position);
                int fragmentsNeeded = (int)(Vector3.Distance(TailAnchorPoint.position, fragments[fragments.Count - 1].transform.position) / offSet.magnitude);
                Debug.Log(fragmentsNeeded + MaxLength + " needed");
            }
        }

        /// <summary>
        /// Get the offSet vector toward the AnchorPoint
        /// </summary>
        /// <param name="_origin">Starting position</param>
        /// <returns>OffSet vector</returns>
        Vector3 GetOffSet(Transform _origin)
        {
            //GetDirection
            Vector3 dir = TailAnchorPoint.position - _origin.position;
            dir = dir.normalized;

            if (fragments.Count <= 1)
            {
                //Measure OffSet magnitude only if first time run of this method
                fragmentDistance = Vector3.Distance(TailAnchorPoint.position, _origin.position) / DensityOfFragments / 100;
            }
            return dir * fragmentDistance;
        }

        #region API
        /// <summary>
        /// Extend the rope if possible
        /// </summary>
        public void ExtendRope()
        {
            //Prevent to extend the rope over MaxLength
            if (fragments.Count >= MaxLength)
                return;
            TailAnchorPoint.GetComponent<ConfigurableJoint>().connectedBody = null;
            BuildRope(fragments[fragments.Count - 1]);
        }
        #endregion
    }
}
