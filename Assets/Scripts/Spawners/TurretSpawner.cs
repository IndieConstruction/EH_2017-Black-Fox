using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class TurretSpawner : SpawnerBase
    {
        new public TurretSpawnerOptions Options;
        Vector3 randomPosition;

        void Update()
        {
            if (Time.time <= 0 && Options.Spawnedturrent <= Options.MaxSpawnTurrent)
            {

                if (Options.Spawnedturrent == Options.MaxSpawnTurrent)
                {
                    Options.Turrent = null;
                }
                else
                {
                    Options.Spawnedturrent++;
                }
            }
        }

        void OnRoundPlay()
        {
            Vector3 randomPosition = new Vector3(Random.Range(-70.0f, 100.0f), 0, Random.Range(-100.0f, 100.0f));
            Instantiate(Options.Turrent, randomPosition, Quaternion.identity);
        }
    }

    public class TurretSpawnerOptions : SpawnerOptions
    {
        public GameObject Turrent;
        public int MaxSpawnTurrent = 4;
        public int Spawnedturrent = 0;
    }
}