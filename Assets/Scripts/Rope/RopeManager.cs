using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rope;

namespace BlackFox {
    public class RopeManager : MonoBehaviour {

        public GameObject RopeOrigin;
        List<GameObject> ropes = new List<GameObject>();
        public Material[] RopeColors = new Material[4];
        #region Event
        /// <summary>
        /// React to OnAgentSpawn by building and connectig to it a new rope
        /// </summary>
        /// <param name="_agent"></param>
        public void ReactToOnAgentSpawn(Avatar _agent)
        {
            AttachNewRope(_agent);
        }
        /// <summary>
        /// React to OnAgentKilled by destroying the rope attached to it
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        public void ReactToOnAgentKilled(Avatar _victim)
        {
            DestroyRope(_victim);
        }
        /// <summary>
        /// React to OnCoreDeath by destroying the rope attached to every player
        /// </summary>
        public void ReactToOnCoreDeath()
        {
            foreach (GameObject rope in ropes)
            {
                Destroy(rope);
            }
        }        
        #endregion

        #region API
        /// <summary>
        /// Create a new Rope and attach it to _target(parameter)
        /// </summary>
        /// <param name="_target"></param>
        public void AttachNewRope(Avatar _target)
        {
            GameObject newOrigin;
            
            newOrigin = Instantiate(RopeOrigin, transform);
            newOrigin.name = _target.PlayerIndex + "Rope";
            //Set the AnchorPoint before the activation of the component
            RopeController rc = newOrigin.GetComponent<RopeController>();
            ConfigurableJoint cj = _target.GetComponent<ConfigurableJoint>();
            rc.AnchorPoint = cj.connectedBody.transform;
            newOrigin.GetComponent<RopeController>().InitRope();
            switch (_target.PlayerIndex)
            {
                case XInputDotNetPure.PlayerIndex.One:
                    newOrigin.GetComponent<LineRenderer>().material = RopeColors[0];
                    break;
                case XInputDotNetPure.PlayerIndex.Two:
                    newOrigin.GetComponent<LineRenderer>().material = RopeColors[1];
                    break;
                case XInputDotNetPure.PlayerIndex.Three:
                    newOrigin.GetComponent<LineRenderer>().material = RopeColors[2];
                    break;
                case XInputDotNetPure.PlayerIndex.Four:
                    newOrigin.GetComponent<LineRenderer>().material = RopeColors[3];
                    break;
                default:
                    break;
            }

            ropes.Add(newOrigin);            
        }
        /// <summary>
        /// Find the Rope attached to _target and destroy it
        /// </summary>
        /// <param name="_target"></param>
        public void DestroyRope(Avatar _target)
        {
            string nameOfRope = _target.PlayerIndex + "Rope";
            foreach (GameObject gObj in ropes)
            {
                //TODO : risolvere sta cagata
                if (gObj != null) {
                    if (gObj.name == nameOfRope) {
                        Destroy(gObj);
                        ropes.Remove(gObj);
                        break;
                    } 
                }                    
            }
        }
    #endregion
    }
}
