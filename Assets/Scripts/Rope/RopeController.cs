using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Rope
{
    [RequireComponent (typeof(LineRenderer),typeof(ConfigurableJoint))]
    public class RopeController : MonoBehaviour
    {
        public GameObject FragmentPrefab;
        public Transform AnchorPoint;
        public int MaxLength = 80;
        [Range(.0f, 10f)]
        public float DensityOfFragments = .1f;

        List<GameObject> fragments = new List<GameObject>();
        LineRenderer lineRend;
        float ropeWidth;
        Vector3 offSet;
        float fragmentDistance;

        void Start()
        {
            if (fragments.Count == 0)
            {
                lineRend = GetComponent<LineRenderer>();
                ropeWidth = GetComponent<LineRenderer>().widthMultiplier;
                fragments.Add(gameObject);
                BuildRope(gameObject);
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < fragments.Count; i++)
            {
                lineRend.SetPosition(i, fragments[i].transform.position);
            }
            if(AnchorPoint != null)
                lineRend.SetPosition(fragments.Count, AnchorPoint.position);
        }

        /// <summary>
        /// Extend the rope toward the AnchorPoint
        /// </summary>
        /// <param name="_lastPiece">Current end of the rope</param>
        void BuildRope(GameObject _lastPiece)
        {
            Vector3 position;
            SphereCollider collider;
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
                collider = newFragment.GetComponent<SphereCollider>();
                collider.radius = ropeWidth / 2;
                //Joint Configuration
                joint = newFragment.GetComponent<ConfigurableJoint>();
                joint.connectedBody = fragments[i - 1].GetComponent<Rigidbody>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = offSet * 0.9f;
                //Is the AnchroPoint closer than the offSet? If so stop building the rope
                if (Vector3.Distance(position, AnchorPoint.position) <= fragmentDistance)
                {
                    joint = AnchorPoint.GetComponent<ConfigurableJoint>();
                    joint.connectedBody = newFragment.GetComponent<Rigidbody>();
                    joint.autoConfigureConnectedAnchor = false;
                    joint.connectedAnchor = offSet;
                    break;
                }
            }

            if (AnchorPoint.GetComponent<ConfigurableJoint>().connectedBody == null) {
                AnchorPoint.GetComponent<ConfigurableJoint>().connectedBody = fragments[fragments.Count - 1].GetComponent<Rigidbody>();
                Debug.Log("WARNING: MaxLength not enough to reach " + AnchorPoint.name + " in " + AnchorPoint.position);
                int fragmentsNeeded = (int)(Vector3.Distance(AnchorPoint.position, fragments[fragments.Count - 1].transform.position) / offSet.magnitude);
                Debug.Log(fragmentsNeeded + MaxLength + " needed");
            }
            lineRend.positionCount = (fragments.Count) + 1;
        }

        /// <summary>
        /// Get the offSet vector toward the AnchorPoint
        /// </summary>
        /// <param name="_origin">Starting position</param>
        /// <returns>OffSet vector</returns>
        Vector3 GetOffSet(Transform _origin)
        {
            //GetDirection
            Vector3 dir = AnchorPoint.position - _origin.position;
            dir = dir.normalized;

            if (fragments.Count <= 1)
            {
                //Measure OffSet magnitude only if first time run of this method
                fragmentDistance = Vector3.Distance(AnchorPoint.position, _origin.position) / DensityOfFragments / 100;
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

            AnchorPoint.GetComponent<ConfigurableJoint>().connectedBody = null;
            BuildRope(fragments[fragments.Count - 1]);
        }
        /// <summary>
        /// Extend the rope by required amount
        /// </summary>
        /// <param name="_extensionAmount">number of fragments to add to the rope</param>
        public void ExtendRope(int _extensionAmount)
        {
            //Prevent to extend the rope over MaxLength
            if (fragments.Count >= MaxLength)
                return;
            //Disconnect the AnchorPoint to fit new rope fragments
            AnchorPoint.GetComponent<ConfigurableJoint>().connectedBody = null;

            Vector3 position;
            SphereCollider collider;
            ConfigurableJoint joint;
            int currAmt = fragments.Count;
            //Relative position of newPieces to previouses
            offSet = GetOffSet(fragments[currAmt -1].transform);

            //Keep building the rope until the AnchorPoint ore the MaxLength are reached
            for (int i = 0; i < _extensionAmount; i++)
            {
                //Add a new Fragment to the rope
                position = fragments[currAmt + i - 1].transform.position + offSet;
                GameObject newFragment = Instantiate(FragmentPrefab, position, Quaternion.LookRotation(position));
                fragments.Add(newFragment);
                newFragment.transform.parent = fragments[0].transform;
                newFragment.name = "Fragment_" + (currAmt +i);
                //Collider configuration
                collider = newFragment.GetComponent<SphereCollider>();
                collider.radius = ropeWidth / 2;
                //Joint Configuration
                joint = newFragment.GetComponent<ConfigurableJoint>();
                joint.connectedBody = fragments[currAmt + i - 1].GetComponent<Rigidbody>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = offSet * 0.9f;                
            }
            //Reconnect the AnchorPoint
            AnchorPoint.GetComponent<ConfigurableJoint>().connectedBody = fragments[fragments.Count - 1].GetComponent<Rigidbody>();
            
            lineRend.positionCount = (fragments.Count) + 1;

        }
        /// <summary>
        /// Initialize the Rope as first launch, preventing missconnection
        /// </summary>
        public void InitRope()
        {
            lineRend = GetComponent<LineRenderer>();
            ropeWidth = GetComponent<LineRenderer>().widthMultiplier;
            fragments.Add(gameObject);
            BuildRope(gameObject);
        }
        #endregion
    }
}
