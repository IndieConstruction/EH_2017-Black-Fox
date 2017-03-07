﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rope
{
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
