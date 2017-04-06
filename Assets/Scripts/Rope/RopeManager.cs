using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rope;

namespace BlackFox {
    public class RopeManager : MonoBehaviour {

        public GameObject RopeAnchorPrefab;
        public Material[] RopeColors = new Material[4];
        private Transform core;

        private void Start()
        {
            if(!core)
                core = FindObjectOfType<Core>().transform;
        }
        #region API
        /// <summary>
        /// Initialize Useful information for RopeManager
        /// </summary>
        /// <param name="_core"></param>
        public void Init(Transform _core)
        {
            core = _core;
        }
        /// <summary>
        /// Create a new Rope and attach it to _target(parameter)
        /// </summary>
        /// <param name="_target"></param>
        public void AttachNewRope(Avatar _target)
        {
            //SetA new AnchorPoint
            Transform anchor = Instantiate(RopeAnchorPrefab, core).transform;
            //Get the Rope of an Agent
            RopeController rc = _target.transform.parent.GetComponentInChildren<RopeController>();
            //Set the rope
            rc.AnchorPoint = anchor;
            rc.InitRope();

            switch (_target.playerIndex)
            {
                case XInputDotNetPure.PlayerIndex.One:
                    rc.GetComponent<LineRenderer>().material = RopeColors[0];
                    break;
                case XInputDotNetPure.PlayerIndex.Two:
                    rc.GetComponent<LineRenderer>().material = RopeColors[1];
                    break;
                case XInputDotNetPure.PlayerIndex.Three:
                    rc.GetComponent<LineRenderer>().material = RopeColors[2];
                    break;
                case XInputDotNetPure.PlayerIndex.Four:
                    rc.GetComponent<LineRenderer>().material = RopeColors[3];
                    break;
                default:
                    break;
            }
        }
    #endregion
    }
}
