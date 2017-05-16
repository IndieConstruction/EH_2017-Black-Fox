using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PowerUpManager : MonoBehaviour
    {

        public float PowerUpLifeTime = 10;

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
                    return Resources.Load("Prefabs/Prefabs/PowerUps/PowerUp_Blue") as GameObject;
                case 2:
                    return Resources.Load("Prefabs/Prefabs/PowerUps/PowerUp_Green") as GameObject;
                case 3:
                    return Resources.Load("Prefabs/Prefabs/PowerUps/PowerUp_Purple") as GameObject;
                case 4:
                    return Resources.Load("Prefabs/Prefabs/PowerUps/PowerUp_Red") as GameObject;
                default:
                    return  Resources.Load("Prefabs/Prefabs/PowerUps/PowerUp_Default") as GameObject;
            }
        }

        #region API

        /// <summary>
        /// Spawna un pawerup in una posizione specifica
        /// </summary>
        /// <param name="_position">La posizione che deve avere il powerup.</param>
        public void SpawnPowerUp(Vector3 _position)
        {
            PowerUpBase tempObj;
            tempObj = Instantiate(ChoosePowerUp(), _position, Quaternion.identity).GetComponent< PowerUpBase>();
            tempObj.LifeTime = PowerUpLifeTime;
        }

        #endregion
    }
}