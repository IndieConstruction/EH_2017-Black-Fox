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
        public Ship BasePrefab;
        public GameObject ModelPrefab;
        public List<ColorSetData> ColorSets;
        public ShipConfig shipConfig;
        public RopeConfig ropeConfig;

        [HideInInspector]
        public int ColorSetIndex;

        [Header("Avatar Upgrades")]
        public AvatarUpgradesConfig avatarUpgradesConfig;
    }
}
