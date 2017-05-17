using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlackFox
{
    public class PowerUpManager : MonoBehaviour
    {

        public float PowerUpLifeTime = 10;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                SpawnPowerUp();
            }
        }

        /// <summary>
        /// Sceglie un powerup a caso
        /// </summary>
        /// <returns></returns>
        GameObject ChoosePowerUp()
        {
            int RandomNum = Random.Range(1, 5);

            switch (RandomNum)
            {
                case 1:
                    return Resources.Load("Prefabs/PowerUps/PowerUp_Blue") as GameObject;
                case 2:
                    return Resources.Load("Prefabs/PowerUps/PowerUp_Green") as GameObject;
                case 3:
                    return Resources.Load("Prefabs/PowerUps/PowerUp_Purple") as GameObject;
                case 4:
                    return Resources.Load("Prefabs/PowerUps/PowerUp_Red") as GameObject;
                default:
                    return  Resources.Load("Prefabs/PowerUps/PowerUp_Default") as GameObject;
            }
        }

        /// <summary>
        /// Spawna un pawerup in una posizione specifica
        /// </summary>
        /// <param name="_position">La posizione che deve avere il powerup.</param>
        void SpawnPowerUp()
        {
            PowerUpBase tempPowerup;
            GameObject tempObj = ChoosePowerUp();
            tempPowerup = Instantiate(tempObj, GameManager.Instance.LevelMng.Core.transform.position, Quaternion.identity).GetComponent< PowerUpBase>();
            DrawParable(tempObj, ChoosePosition(GameManager.Instance.PlayerMng.Players));
            if(tempPowerup != null)
                tempPowerup.LifeTime = PowerUpLifeTime;
        }

        void DrawParable(GameObject _objToMove, Vector3 _target)
        {
            _objToMove.transform.DOMoveX(_target.x,0.5f);
            _objToMove.transform.DOMoveZ(_target.z, 0.5f);
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