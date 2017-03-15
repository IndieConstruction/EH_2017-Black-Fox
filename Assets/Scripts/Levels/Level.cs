using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    [CreateAssetMenu(fileName = "LevelName", menuName = "Levels/NewLevel", order = 1)]
    public class Level : ScriptableObject {

        public string LevelName;
        public GameObject ArenaPrefab;
        public List<SpawnerBase> Spawners = new List<SpawnerBase>();
    } 
}
