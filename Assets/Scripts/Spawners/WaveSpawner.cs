using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    /// <summary>
    /// Spawn a Wave on a random SpawnPoint between the min/max given time
    /// </summary>
    public class WaveSpawner : SpawnerBase {
        public List<Transform> SpawnPoints = new List<Transform>();
        GameObject wave;
        float nextTime;
        new public WaveSpawnerOptions Options;

        public override SpawnerBase Init(SpawnerOptions options) {
            Options = options as WaveSpawnerOptions;
            return this;
        }

        void Update() {
            if (Time.time >= nextTime) {
                int spawn = Random.Range(0, SpawnPoints.Count);
                InstantiateWave(SpawnPoints[spawn]);
                nextTime += Random.Range(Options.MinTime, Options.MaxTime);
            }
        }

        void OnDisable() {
            Destroy(wave);
        }

        void InstantiateWave(Transform _spawn) {
            if (!wave)
                wave = Instantiate(Options.WavePrefab, _spawn.position, _spawn.rotation);
            else {
                wave.transform.position = _spawn.position;
                wave.transform.rotation = _spawn.rotation;
            }
        }
    }

    [System.Serializable]
    public class WaveSpawnerOptions : SpawnerOptions {
        public GameObject WavePrefab;
        public float MinTime = 20;
        public float MaxTime = 50;
    }
}
