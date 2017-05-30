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
            PowerUps = Resources.LoadAll<GameObject>("Prefabs/PowerUps").ToList();
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

        /// <summary>
        /// Sceglie un powerup a caso
        /// </summary>
        /// <returns></returns>
        GameObject ChoosePowerUp()
        {
            return PowerUps[Random.Range(1, PowerUps.Count)];
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

        void DrawParable(GameObject _objToMove, Vector3 _target)
        {
            _objToMove.transform.DOJump(_target, 50, 1, 1f).OnComplete(() => { /*_objToMove.GetComponent<Collider>().enabled = true;*/ AddCollider(_objToMove); });
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
            finalPosition.x = (GameManager.Instance.LevelMng.Core.transform.position.x - finalPosition.x) * Random.Range(0f, 4f) / players.Count; ;
            finalPosition.z = (GameManager.Instance.LevelMng.Core.transform.position.z - finalPosition.z) * Random.Range(0f, 4f) / players.Count; ;

            return finalPosition;
        }

    }
}