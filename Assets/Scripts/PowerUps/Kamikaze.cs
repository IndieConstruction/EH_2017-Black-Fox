using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class Kamikaze : PowerUpBase
    {
        protected override void Init()
        {
            base.Init();
            SpawnRatio = GameManager.Instance.LevelMng.CurrentLevel.RatioKamikaze;
        }
        public override void UsePowerUp()
        {
            foreach (SpawnerBase spawner in GameManager.Instance.LevelMng.SpawnerMng.Spawners)
            {
                if (spawner.ID == "ExternalElementSpawner")
                {
                    (spawner as ExternalElementSpawner).ActiveKamikazeTime(PowerUpDuration);
                }

            }
        }
    }
}