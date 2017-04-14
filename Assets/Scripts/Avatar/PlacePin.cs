using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlacePin : MonoBehaviour
    {
        public GameObject PinPrefab;
        public Transform PinSpanw;
        public float CoolDownTime;
        [HideInInspector]
        public bool CanPlace = true;

        float xValue;
        float prectime;

        private void Start()
        {
            xValue = PinSpanw.localPosition.x;
        }

        private void Update()
        {
            prectime -= Time.deltaTime;
        }

        /// <summary>
        /// Instantiate the pin on the PinSpawn
        /// </summary>
        public void placeThePin(Avatar _owner, bool _isRight)
        {
            if (prectime <= 0 && CanPlace == true)
            {
                SetPinSpawnPosition(_isRight);
                Instantiate(PinPrefab, PinSpanw.position, PinSpanw.rotation, GameManager.Instance.LevelMng.PinsContainer);
                _owner.AddShooterAmmo();
                prectime = CoolDownTime;
            }
        }

        /// <summary>
        /// Change the position of the PinSpawnPoint
        /// </summary>
        /// <param name="_isRight"></param>
        public void SetPinSpawnPosition(bool _isRight)
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
}
