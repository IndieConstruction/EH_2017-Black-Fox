using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlacePin : MonoBehaviour
    {
        [HideInInspector]
        public bool CanPlace = true;

        public Transform PinSpanw;

        PlacePinConfig placePinConfig
        {
            get { return ship.avatar.AvatarData.shipConfig.placePinConfig; }
        }

        Ship ship;

        float xValue;
        float prectime;

        private void Start()
        {
            xValue = PinSpanw.localPosition.x;
        }

        private void Update()
        {
            prectime -= Time.deltaTime;
            //if (prectime <= 0)
            //    StartCoroutine(Vibrate(0.2f));
        }

        #region API
        public void Init(Ship _owner)
        {
            ship = _owner;
        }
        
        /// <summary>
        /// Instantiate the pin on the PinSpawn
        /// </summary>
        public void placeThePin(bool _isRight)
        {
            if (prectime <= 0 && CanPlace == true)
            {
                SetPinSpawnPosition(_isRight);
                Instantiate(placePinConfig.PinPrefab, PinSpanw.position, PinSpanw.rotation, GameManager.Instance.LevelMng.PinsContainer);
                ship.AddShooterAmmo();
                prectime = placePinConfig.CoolDownTime;
            }
        }
        #endregion

        IEnumerator Vibrate(float _rumbleTime)
        {
            // TODO : togliere la vibrazione durante il count down (da fare nel refactoring dell'avatar)
            if(!GameManager.Instance.LevelMng.IsGamePaused)
               ship.avatar.Player.ControllerVibration(0.5f, 0.5f);

            yield return new WaitForSeconds(_rumbleTime);
            ship.avatar.Player.ControllerVibration(0f, 0f);
        }

        /// <summary>
        /// Change the position of the PinSpawnPoint
        /// </summary>
        /// <param name="_isRight"></param>
        void SetPinSpawnPosition(bool _isRight)
        {
            if (!_isRight)
            {
                PinSpanw.localPosition = new Vector3(-xValue, PinSpanw.localPosition.y, PinSpanw.localPosition.z);
            }
            else
            {
                PinSpanw.localPosition = new Vector3(xValue, PinSpanw.localPosition.y, PinSpanw.localPosition.z);
            }
        }

    }

    [Serializable]
    public class PlacePinConfig
    {
        public GameObject PinPrefab;
        public float CoolDownTime;
    }
}
