using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    [CreateAssetMenu(fileName = "ColorSetData", menuName = "Avatar/Colors", order = 2)]
    public class ColorSetData : ScriptableObject {

        public Material ShipMaterialMain;
        public Material RopeMaterial;

    }
}