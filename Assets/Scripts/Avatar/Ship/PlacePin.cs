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
                if (ship.Avatar.AvatarData.shipConfig.placePinConfig == null)
                    data = new PlacePinConfig();
                else
                    data = ship.Avatar.AvatarData.shipConfig.placePinConfig;
                return data; }
        }
        List<GameObject> pinsPlaced = new List<GameObject>();
        Transform initialTransf;
        Ship ship;
        float prectime;
        bool isRecharging = false;

        private void Update()
        {
            if (ship != null)
            {
                if (!GameManager.Instance.LevelMng.IsGamePaused || GameManager.Instance.LevelMng.IsRoundActive)
                {
                    prectime -= Time.deltaTime;
                    if (prectime <= 0 && !isRecharging)
                    {
                        isRecharging = true;
                        StartCoroutine(Rumble(0.2f));
                    }
                }                
            }
        }

        IEnumerator Rumble(float _rumbleTime)
        {
            ship.Avatar.Player.ControllerVibration(0.5f, 0.5f);
            yield return new WaitForSeconds(_rumbleTime);
            ship.Avatar.Player.ControllerVibration(0f, 0f);
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


        public float CurrentPinRate
        {
            get
            {
                if (ship.Avatar.GetUpgrade(UpgardeTypes.PinRegenUpgrade) != null)
                    return ship.Avatar.GetUpgrade(UpgardeTypes.PinRegenUpgrade).CalculateValue(placePinConfig.CoolDownTime);
                else
                    return placePinConfig.CoolDownTime;
            }

        }
        #region API
        /// <summary>
        /// Set working values for the componet
        /// </summary>
        /// <param name="_owner"></param>
        public void Setup(Ship _owner)
        {
            ship = _owner;
            prectime = CurrentPinRate;
            initialTransf = transform;
        }

        /// <summary>
        /// Instantiate the pin on the PinSpawn (true/false switch between right/left)
        /// </summary>
        public void PlaceThePin()
        {
            if (prectime <= 0 && canPlace == true)
            {
                GameObject pin = Instantiate(placePinConfig.PinPrefab, transform.position + transform.forward*placePinConfig.DistanceFromShipOrigin, transform.rotation);
                pinsPlaced.Add(pin);
                ship.AddShooterAmmo();
                isRecharging = false;
                foreach (Renderer pinRend in pin.GetComponentsInChildren<Renderer>())
                {
                    pinRend.material = ship.Avatar.AvatarData.ColorSets[ship.Avatar.AvatarData.ColorSetIndex].PinMaterial;
                }
                pin.transform.parent = GameManager.Instance.LevelMng.PinsContainer;
                prectime = CurrentPinRate;
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
    }

    [Serializable]
    public class PlacePinConfig
    {
        public GameObject PinPrefab;
        public float CoolDownTime = 3;
        public float DistanceFromShipOrigin;
    }
}
