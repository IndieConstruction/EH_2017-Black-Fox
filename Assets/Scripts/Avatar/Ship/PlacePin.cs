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
        List<GameObject> pinsPlaced = new List<GameObject>();
        Transform initialTransf;
        Ship ship;
        float prectime;
        bool isRight;
        bool isRecharging = false;

        private void Update()
        {
            if (ship != null)
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
            initialTransf = transform;
        }

        /// <summary>
        /// Instantiate the pin on the PinSpawn (true/false switch between right/left)
        /// </summary>
        public void PlaceThePin(bool _placeRight)
        {
            SetPinSpawnPosition(_placeRight);
            if (prectime <= 0 && canPlace == true)
            {
                GameObject pin = Instantiate(placePinConfig.PinPrefab, transform.position , Quaternion.identity);
                pinsPlaced.Add(pin);
                foreach (Renderer pinRend in pin.GetComponentsInChildren<Renderer>())
                {
                    pinRend.material = ship.avatar.AvatarData.shipConfig.ColorSets[ship.avatar.ColorSetIndex].PinMaterial;
                }
                pin.transform.parent = GameManager.Instance.LevelMng.PinsContainer;
                prectime = placePinConfig.CoolDownTime;
            }
        }
        /// <summary>
        /// Remove all the placed Pins
        /// </summary>
        public void RemoveAllPins()
        {
            foreach (GameObject pin in pinsPlaced)
            {
                Destroy(pin);
            }
            pinsPlaced.Clear();
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
        void SetPinSpawnPosition(bool _isRight)
        {
            if (_isRight && !isRight)
            {
                transform.localPosition = initialTransf.localPosition;
                isRight = _isRight;
            }
            else if(!_isRight && isRight)
            {
                transform.localPosition = new Vector3(-initialTransf.localPosition.x, initialTransf.localPosition.y, initialTransf.localPosition.z);
                isRight = _isRight;
            }
        }
    }

    [Serializable]
    public class PlacePinConfig
    {
        public GameObject PinPrefab;
        public float CoolDownTime = 3;
    }
}
