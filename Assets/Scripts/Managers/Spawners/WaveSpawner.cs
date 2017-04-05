using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    /// <summary>
    /// Spawn a Wave on a random SpawnPoint between the min/max given time
    /// </summary>
    public class WaveSpawner : SpawnerBase {
        List<Transform> SpawnPoints = new List<Transform>();
        GameObject wave;
        float nextTime;
        bool active;
        new public WaveSpawnerOptions Options;

        public override SpawnerBase Init(SpawnerOptions options) {
            Options = options as WaveSpawnerOptions;
            return this;
        }

        private void Start()
        {
            nextTime += Random.Range(Options.MinTime, Options.MaxTime);
            foreach (var spawn in FindObjectsOfType<SpawnPoint>())
                foreach (var item in spawn.ValidAs)
                    if (item == SpawnPoint.SpawnType.WaveSpawn)
                        SpawnPoints.Add(spawn.transform);       
        }

        void Update() {
            if(active)
                Wave();            
        }
        
        void Wave()
        {
            if (Time.time >= nextTime)
            {
                int spawn = Random.Range(0, SpawnPoints.Count);
                InstantiateWave(SpawnPoints[spawn]);
                nextTime += Random.Range(Options.MinTime, Options.MaxTime);
            }
        }
        /// <summary>
        /// Place a new wave in Scene or move the current one onto a different spawnpoit
        /// </summary>
        /// <param name="_spawn"></param>
        void InstantiateWave(Transform _spawn) {
            if (!wave)
                wave = Instantiate(Options.WavePrefab, _spawn.position, _spawn.rotation);
            else {
                wave.transform.position = _spawn.position;
                wave.transform.rotation = _spawn.rotation;
            }
        }

        #region API
        public override void Toggle()
        {
            active = !active;
        }
        public override void Restart()
        {
            CleanSpawned();

            int spawn = Random.Range(0, SpawnPoints.Count);
            InstantiateWave(SpawnPoints[spawn]);
            nextTime += Random.Range(Options.MinTime, Options.MaxTime);

            Toggle();
        }
        public override void CleanSpawned()
        {
            if (wave)
                Destroy(wave);
        }
        #endregion
    }

    [System.Serializable]
    public class WaveSpawnerOptions : SpawnerOptions {
        public GameObject WavePrefab;
        public float MinTime = 20;
        public float MaxTime = 50;
    }
}
