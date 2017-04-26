using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rope;

namespace BlackFox
{
    /// <summary>
    /// Informazioni riguardo a ciò che contiene l'avatar
    /// </summary>
    [CreateAssetMenu(fileName = "AvatarData", menuName = "Avatar/NewShip", order = 1)]
    public class AvatarData : ScriptableObject
    {
        // TODO : fare rope config
        //public RopeConfig RopeCtrl; 
        public ShipConfig shipConfig;
    }
}
