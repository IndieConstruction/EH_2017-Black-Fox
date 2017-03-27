using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class TurretSpawner : SpawnerBase
    {

        float NordWall = 59.67f;
        float EstWall = 89.51f;
        public float Width = 20f;
        float RandX;
        float RandZ;
        Vector3 InstacePosition;
        public GameObject Turret;

        //Like Start()
        protected override void OnActivation()
        {
            RandX = Random.Range(-Width, Width);
            RandZ = Random.Range(-Width, Width);
            if (RandX > 0)
            {
                RandX = EstWall - RandX;
            }
            else
            {
                RandX = - RandX - EstWall;
            }
            if (RandZ > 0) {
                RandZ = NordWall - RandZ;
            }
            else
            {
                RandZ = - RandZ - NordWall;
            }

            InstacePosition = new Vector3(RandX, 0, RandZ);
            Debug.Log(InstacePosition);
        }

        //Like Update()
        protected override void OnRuntime()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GenerateTurret();
            }
        }

        void GenerateTurret()
        {
            Instantiate(Turret, InstacePosition, Quaternion.identity);
            Debug.Log("Istanzio");
        }

    }
}