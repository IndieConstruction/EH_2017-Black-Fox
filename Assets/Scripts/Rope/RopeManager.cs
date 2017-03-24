using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rope
{
    public class RopeManager : MonoBehaviour
    {
        public List<RopeController> RopeControllers = new List<RopeController>(4);

        LineRenderer[] renderers;

    }
}
