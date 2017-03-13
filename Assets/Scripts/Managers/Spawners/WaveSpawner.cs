using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    /// <summary>
    /// Spawn a Wave on a random SpawnPoint between the min/max given time
    /// </summary>
    public class WaveSpawner : SpawnerBase
    {
        public List<Transform> SpawnPoints = new List<Transform>();
        GameObject wave;
        float nextTime;
        public GameObject WavePrefab;
        public float MinTime = 20;
        public float MaxTime = 50;

        protected override void OnRuntime()
        {
            if (Time.time >= nextTime)
            {
                int spawn = Random.Range(0, SpawnPoints.Count);
                InstantiateWave(SpawnPoints[spawn]);
                nextTime += Random.Range(MinTime, MaxTime);
            }
        }

        protected override void OnDeactivation()
        {
            Destroy(wave);
        }

        void InstantiateWave(Transform _spawn)
        {
            if (!wave)
                wave = Instantiate(WavePrefab, _spawn.position, _spawn.rotation);
            else
            {
                wave.transform.position = _spawn.position;
                wave.transform.rotation = _spawn.rotation;
            }
        }
    }
}
