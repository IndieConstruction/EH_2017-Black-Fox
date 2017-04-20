using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    [CreateAssetMenu(fileName = "ShipModel", menuName = "Ships/NewShip", order = 1)]
    public class ShipModel : ScriptableObject
    {         
        public Mesh mesh;
        public Material material;
    }
}
