using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    [CreateAssetMenu(fileName = "LevelName", menuName = "Levels/NewLevel", order = 1)]
    public class Level : ScriptableObject {

        public int Order;
        public string LevelName;
        public GameObject ArenaPrefab;

        [Header("Spawners")]
        public ArrowsSpawerOptions ArrowsSpawner;
        public AvatarSpawnerOptions AvatarSpawner;
        public BlackHoleSpawnerOptions BlackHoleSpawner;
        public ExternalElementOptions ExternalElementSpawner;
        public TurretSpawnerOptions TurretSpawner;
        public WaveSpawnerOptions WaveSpawner;
    } 
}
