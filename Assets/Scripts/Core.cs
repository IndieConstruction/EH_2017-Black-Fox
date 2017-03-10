using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class Core : MonoBehaviour, IDamageable
    {

        float life;
        public float MaxLife = 10;              // La vita massima che può avere il Core e che viene impostata al riavvio di un round perso
        GameManager gameManager;
        GameObject Obstacle;
        public Transform ObstacleSpawn1;
        public Transform ObstacleSpawn2;
        public bool Level4 = false;

        int RandomNum;

        public float Life
        {
            get { return life; }
            set { life = value; }
        }

        private void Start()
        {
            Obstacle = Resources.Load<GameObject>("Prefabs/LevelElements/OstacoloMobile");
            RandomNum = (int)UnityEngine.Random.Range(1f, 3f);
            gameManager = GameManager.Instance;
            if (gameManager.CoreLife == 0)
                life = MaxLife;
            else
                life = gameManager.CoreLife;
            if (Level4 == true)
            {
                instantiateObstacles();
            }

            //if (gameManager.GetGameUIController() != null)
            //{
            //    gameManager.CoreSliderValueUpdate(life, MaxLife);
            //}
        }

        void instantiateObstacles()
        {
            switch (RandomNum)
            {
                case 1:
                    Instantiate(Obstacle, ObstacleSpawn1.position, ObstacleSpawn1.rotation);
                    break;
                case 2:
                    Instantiate(Obstacle, ObstacleSpawn2.position, ObstacleSpawn2.rotation);
                    break;
                default:
                    break;
            }

        }


        #region Interfacce

        public void Damage(float _damage, GameObject _attacker)
        {
            life -= _damage;
            //if (gameManager.GetGameUIController() != null)
            //    gameManager.CoreSliderValueUpdate(life, MaxLife);
            if (life < 1)
            {
                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;
                gameManager.ReloadScene();
            }
        }

        #endregion
    }
}