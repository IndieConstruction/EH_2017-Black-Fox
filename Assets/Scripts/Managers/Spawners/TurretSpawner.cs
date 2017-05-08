﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class TurretSpawner : SpawnerBase
    {
        new public TurretSpawnerOptions Options;
        public int Spawnedturrent = 0;

        void Update()
        {
            if (Time.time <= 0 && Spawnedturrent <= Options.MaxSpawnTurrent)
            {

                if (Spawnedturrent == Options.MaxSpawnTurrent)
                {
                    Options.Turrent = null;
                }
                else
                {
                    Spawnedturrent++;
                }
            }
        }

        // TODO : chiamare la funzione a inizio round (controllare cos'è)
        void OnRoundPlay()
        {
            Vector3 randomPosition = new Vector3(Random.Range(-70.0f, 100.0f), 0, Random.Range(-100.0f, 100.0f));
            Instantiate(Options.Turrent, randomPosition, Quaternion.identity);
        }
    }

    [System.Serializable]
    public class TurretSpawnerOptions : SpawnerOptions
    {
        public GameObject Turrent;
        public int MaxSpawnTurrent = 4;
    }
}