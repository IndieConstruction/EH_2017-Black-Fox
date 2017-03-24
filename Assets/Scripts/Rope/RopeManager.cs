﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rope;

namespace BlackFox {
    public class RopeManager : MonoBehaviour {

        public GameObject RopeOrigin;
        List<GameObject> ropes = new List<GameObject>();

    #region API
        /// <summary>
        /// Create a new Rope and attach it to _target(parameter)
        /// </summary>
        /// <param name="_target"></param>
        public void AttachNewRope(GameObject _target)
        {
            GameObject newOrigin;
            Agent trgAgent = _target.GetComponentInChildren<Agent>();
            Transform anchorTransform = null;
            foreach (var item in _target.GetComponentsInChildren<ConfigurableJoint>())
            {
                if (item.gameObject.layer == 8) //Search for the Rope (layer 8) AnchorPoint
                {
                    anchorTransform = item.transform;
                    break;
                }
            }
            
            newOrigin = Instantiate(RopeOrigin, transform);
            newOrigin.name = trgAgent.playerIndex + "Rope";
            //Set the AnchorPoint before the activation of the component
            newOrigin.GetComponent<RopeController>().AnchorPoint = anchorTransform;
            newOrigin.GetComponent<RopeController>().enabled = true;

            ropes.Add(newOrigin);
        }
        /// <summary>
        /// Find the Rope attached to _target and destroy it
        /// </summary>
        /// <param name="_target"></param>
        public void DestroyRope(GameObject _target)
        {
            string nameOfRope = _target.GetComponentInChildren<Agent>().playerIndex + "Rope";
            foreach (GameObject gObj in ropes)
            {
                if (gObj.name == nameOfRope)
                {
                    Destroy(gObj);
                    break;
                }                    
            }
        }
    #endregion
    }
}
