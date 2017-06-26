﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    [CreateAssetMenu(fileName = "ColorSetData", menuName = "Avatar/NewColorSet", order = 2)]
    public class ColorSetData : ScriptableObject
    {
        public bool IsPurchased;
        public Material ShipMaterialMain;
        public Material RopeMaterial;
        public Material PinMaterial;
        public Sprite HudColor;
    }
}