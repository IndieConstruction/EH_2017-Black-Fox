using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    [CreateAssetMenu(fileName = "AvatarData", menuName = "Avatar/NewShip", order = 1)]
    public class AvatarData : ScriptableObject
    {
        public ShipConfig shipConfig;
    }
}
