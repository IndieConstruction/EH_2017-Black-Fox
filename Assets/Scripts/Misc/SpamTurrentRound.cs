using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class SpamTurrentRound : SpawnerBase 
    {

        public GameObject turrent;
        public int MaxSpawnTurrent = 4;
        public int Spawnedturrent = 0;
        Vector3 randomPosition;


        // instanzia il prefab fra  -10.0 and 10.0 sul piano x-z  
        protected override void OnActivation()
        {

            Vector3 randomPosition= new Vector3(Random.Range(-70.0f, 100.0f), 0, Random.Range(-100.0f, 100.0f));
            Instantiate(turrent, randomPosition, Quaternion.identity);

          }

        protected override void OnRuntime()
        {
            if(Time.time <=0 && Spawnedturrent <= MaxSpawnTurrent)
            {

                if (Spawnedturrent == MaxSpawnTurrent)
                {
                    turrent = null;
                }
                else
                {
                    Spawnedturrent++;
                }


            }

            





        }



    }
}
