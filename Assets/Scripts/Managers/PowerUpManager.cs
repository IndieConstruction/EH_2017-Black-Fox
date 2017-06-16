using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace BlackFox
{
    public class PowerUpManager : MonoBehaviour
    {

        public float PowerUpLifeTime = 10;
        public float MinZ = 0;
        public float MaxZ = 62;
        public float MinX = 0;
        public float MaxX = 32;
        List<GameObject> PowerUps = new List<GameObject>();
        bool IsActive = false;
        GameObject container;
        float MinTimeToSpawn
        {
            get { return GameManager.Instance.LevelMng.CurrentLevel.MinPowerUpRatio; }
        }
        float MaxTimeToSpawn
        {
            get { return GameManager.Instance.LevelMng.CurrentLevel.MaxPowerUpRatio; }
        }
        float timer {
            get
            {
                return Random.Range(MinTimeToSpawn, MaxTimeToSpawn);
            }
        }

        float countdown;

        float PowerupRatioSum;

        private void Update()
        {
            if (IsActive)
            {
                countdown -= Time.deltaTime;
                if (countdown <= 0)
                {
                    SpawnPowerUp();
                    countdown = timer;
                }
            }
        }

        #region API
        public void Init()
        {
            PowerUps = Resources.LoadAll<GameObject>("Prefabs/PowerUps").Where(p => p.GetComponent<PowerUpBase>() != null).ToList();
            CalculatePercentage();
            container = new GameObject("PowerUpContainer");
            container.transform.parent = transform;
            countdown = timer;
        }

        public void Toggle(bool _value)
        {
            IsActive = _value;
        }

        public void CleanSpawned()
        {
            if (container != null)
                Destroy(container); 
        }

        #endregion

        #region PowerUpSpawn

        List<PowerupsPercentage> powerUpPercentages = new List<PowerupsPercentage>();

        /// <summary>
        /// Calcola la percentuale di probabilità con cui può essere spawnato
        /// </summary>
        void CalculatePercentage()
        {
            List<PowerUpBase> tempPowerUps = new List<PowerUpBase>();
            foreach (GameObject item in PowerUps)
            {
                PowerUpBase powerup = item.GetComponent<PowerUpBase>();
                PowerupRatioSum += powerup.SpawnRatio;
                tempPowerUps.Add(powerup);
            }

            for (int i = 0; i < tempPowerUps.Count; i++)
            {
                powerUpPercentages.Add(new PowerupsPercentage { PowerUpID = tempPowerUps[i].ID.ToString(), Percentage = (tempPowerUps[i].SpawnRatio * 100) / PowerupRatioSum });
            }
        }


        /// <summary>
        /// Sceglie un pawerup in base alla percentuale di probabilità che abbia di essere spawnato
        /// </summary>
        /// <returns></returns>
        GameObject ChoosePowerUp()
        {
            float randNum = Random.Range(0, PowerupRatioSum);
            float tempMinValue = 0f;

            for (int i = 0; i < powerUpPercentages.Count; i++)
            {
                if(randNum < (powerUpPercentages[i].Percentage + tempMinValue) && randNum >= tempMinValue)
                {
                    foreach (GameObject item in PowerUps)
                    {
                        if (item.GetComponent<PowerUpBase>().ID.ToString() == powerUpPercentages[i].PowerUpID)
                            return item;
                    } 
                }
                tempMinValue += powerUpPercentages[i].Percentage;
            }
            return null;
        }

        /// <summary>
        /// Spawna un pawerup in una posizione specifica
        /// </summary>
        /// <param name="_position">La posizione che deve avere il powerup.</param>
        void SpawnPowerUp()
        {
            PowerUpBase tempPowerup;
            GameObject tempObj = ChoosePowerUp();
			tempPowerup = Instantiate(tempObj, container.transform).GetComponent<PowerUpBase>();
			// modifica la rotazione del powerup riportandola a 0,0,0
            //tempPowerup = Instantiate(tempObj, GameManager.Instance.LevelMng.Core.transform.position, Quaternion.identity, container.transform).GetComponent<PowerUpBase>();
            //if(tempPowerup.GetComponent<Collider>())
            //    tempPowerup.GetComponent<Collider>().enabled = false;
            DrawParable(tempPowerup.gameObject, ChoosePosition(GameManager.Instance.PlayerMng.Players));
            if (tempPowerup != null)
                tempPowerup.LifeTime = PowerUpLifeTime;
        }
        
        #endregion

        void DrawParable(GameObject _objToMove, Vector3 _target)
        {
            _objToMove.transform.DOJump(_target, 50, 1, 1f).OnComplete(() => {AddCollider(_objToMove); });
        }

        void AddCollider(GameObject _obj)
        {
            _obj.AddComponent<SphereCollider>().isTrigger = true;
        }

        Vector3 ChoosePosition(List<Player> players)
        {
            Vector3 finalPosition = new Vector3();
            for (int i = 0; i < players.Count; i++)
            {
                finalPosition = finalPosition + players[i].Avatar.ship.transform.position;
            }
            finalPosition.y /= players.Count;
            finalPosition.x = (GameManager.Instance.LevelMng.Core.transform.position.x - finalPosition.x) * Random.Range(0f, 2f) / players.Count; ;
            finalPosition.z = (GameManager.Instance.LevelMng.Core.transform.position.z - finalPosition.z) * Random.Range(0f, 2f) / players.Count; ;

            //Check becero per tenere i power Up in scena
            if (finalPosition.magnitude < new Vector3(MinX, 8, MinZ).magnitude)
            {
                finalPosition += Vector3.one;
                finalPosition *= 2;
            }

            if (finalPosition.x > MaxX)
                finalPosition.x = MaxX;
            if (finalPosition.x < -MaxX)
                finalPosition.x = -MaxX;

            if (finalPosition.z > MaxZ)
                finalPosition.z = MaxZ;
            if (finalPosition.z < -MaxZ)
                finalPosition.z = -MaxZ;

            return finalPosition;
        }

    }

    struct PowerupsPercentage
    {
        public string PowerUpID;
        public float Percentage;
    }

}