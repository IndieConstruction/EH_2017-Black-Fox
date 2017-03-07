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
        public Transform ObstacleSpawn3;
        public Transform ObstacleSpawn4;
        int RandomNum;

        public float Life
        {
            get { return life; }
            set { life = value; }
        }

        private void Start()
        {
            RandomNum = (int)UnityEngine.Random.Range(1f, 4f);
            gameManager = GameManager.Instance;
            if (gameManager.CoreLife == 0)
                life = MaxLife;
            else
                life = gameManager.CoreLife;

            if (gameManager.GetGameUIController() != null)
                gameManager.CoreSliderValueUpdate(life, MaxLife);
        }

        void instantiateObstacles()
        {
            //Instantiate(Obstacle, )
        }


        #region Interfacce

        public void Damage(float _damage, GameObject _attacker)
        {
            life -= _damage;
            if (gameManager.GetGameUIController() != null)
                gameManager.CoreSliderValueUpdate(life, MaxLife);
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