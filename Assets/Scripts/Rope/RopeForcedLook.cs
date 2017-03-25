using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rope
{
    /// <summary>
    /// Keep the GameObj looking at the Target
    /// </summary>
    public class RopeForcedLook : MonoBehaviour
    {
        public GameObject Target;
        private void FixedUpdate()
        {
            if (Target != null)
                transform.LookAt(Target.transform);
        }
    }
}
