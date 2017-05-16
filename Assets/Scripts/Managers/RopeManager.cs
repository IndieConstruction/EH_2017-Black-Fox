using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rope;

namespace BlackFox {
    public class RopeManager : MonoBehaviour {
        public GameObject RopeOrigin;        

        #region API
        /// <summary>
        /// Create a new Rope and attach it to _target(parameter)
        /// </summary>
        /// <param name="_target"></param>
        public void AttachNewRope(Avatar _target, Rigidbody _originRigid = null)
        {
            GameObject newOrigin;
            Transform originPos;
            if (_originRigid == null)
                originPos = transform;
            else
                originPos = _originRigid.transform;

            newOrigin = Instantiate(RopeOrigin, originPos.position, originPos.rotation, _target.transform);
            newOrigin.name = _target.PlayerId + "Rope";
            
            if (_originRigid == null)
                newOrigin.GetComponent<ConfigurableJoint>().connectedBody = _originRigid;

            //Set the AnchorPoint before the activation of the component
            RopeController rc = newOrigin.GetComponent<RopeController>();
            _target.rope = rc;

            rc.AnchorPoint = _target.ship.transform;
            newOrigin.GetComponent<RopeController>().InitRope();
            
            newOrigin.GetComponent<LineRenderer>().material = _target.AvatarData.ColorSets[_target.ColorSetIndex].RopeMaterial;    
        }        
    #endregion
    }
}
