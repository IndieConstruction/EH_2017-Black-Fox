using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class Kamikaze : PowerUpBase
    {
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