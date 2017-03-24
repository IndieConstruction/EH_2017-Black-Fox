using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rope
{
    public class RopeManager : MonoBehaviour
    {
        public List<RopeController> RopeControllers = new List<RopeController>(4);

        LineRenderer[] renderers;

        //private void LateUpdate()
        //{
        //    for (int i = 0; i < fragments.Count; i++)
        //    {
        //        lineRend.SetPosition(i, fragments[i].transform.position);
        //    }
        //    lineRend.SetPosition(fragments.Count, TailAnchorPoint.position);
        //}
    }
}
