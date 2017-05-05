using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlacePin : MonoBehaviour
    {
        protected bool canPlace = true;

        PlacePinConfig placePinConfig
        {
            get {
                PlacePinConfig data;
                if (ship.avatar.AvatarData.shipConfig.placePinConfig == null)
                    data = new PlacePinConfig();
                else
                    data = ship.avatar.AvatarData.shipConfig.placePinConfig;
                return data; }
        }
        Ship ship;
        float prectime;
        bool isRecharging = false;

        private void Update()
        {
            if(ship != null)
            {
                if (!GameManager.Instance.LevelMng.IsGamePaused)
                    prectime -= Time.deltaTime;
                if (prectime <= 0 && !isRecharging)
                {
                    StartCoroutine(Vibrate(0.2f));
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "PinBlockArea")
                canPlace = false;
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "PinBlockArea")
                canPlace = true;
        }

        #region API
        /// <summary>
        /// Set working values for the componet
        /// </summary>
        /// <param name="_owner"></param>
        public void Setup(Ship _owner)
        {
            ship = _owner;
            prectime = placePinConfig.CoolDownTime;
        }
        
        /// <summary>
        /// Instantiate the pin on the PinSpawn
        /// </summary>
        public void PlaceThePin(bool _placeRight)
        {
            if (prectime <= 0 && canPlace == true)
            {
                Debug.Log(GetPinSpawnPosition(_placeRight));
                GameObject pin = Instantiate(placePinConfig.PinPrefab, GetPinSpawnPosition(_placeRight), Quaternion.identity);
                pin.transform.parent = GameManager.Instance.LevelMng.PinsContainer;
                prectime = placePinConfig.CoolDownTime;
            }
        }
        #endregion

        IEnumerator Vibrate(float _rumbleTime)
        {
            isRecharging = true;
            // TODO : togliere la vibrazione durante il count down (da fare nel refactoring dell'avatar)
            ship.avatar.Player.ControllerVibration(0.5f, 0.5f);

            yield return new WaitForSeconds(_rumbleTime);
            isRecharging = false;
            ship.avatar.Player.ControllerVibration(0f, 0f);
        }

        /// <summary>
        /// Change the position of the PinSpawnPoint
        /// </summary>
        /// <param name="_isRight"></param>
        Vector3 GetPinSpawnPosition(bool _isRight)
        {
            if (_isRight)
                return transform.localPosition;
            else
                return new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
    }

    [Serializable]
    public class PlacePinConfig
    {
        public GameObject PinPrefab;
        public float CoolDownTime = 3;
    }
}
