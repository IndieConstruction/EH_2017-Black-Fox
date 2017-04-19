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

        Avatar owner;

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
        public void SetOwner(Avatar _owner)
        {
            owner = _owner;
        }
        
        /// <summary>
        /// Instantiate the pin on the PinSpawn
        /// </summary>
        public void placeThePin(bool _isRight)
        {
            if (prectime <= 0 && CanPlace == true)
            {
                SetPinSpawnPosition(_isRight);
                Instantiate(PinPrefab, PinSpanw.position, PinSpanw.rotation, GameManager.Instance.LevelMng.PinsContainer);
                owner.AddShooterAmmo();
                prectime = CoolDownTime;
            }
        }
        #endregion

        IEnumerator Vibrate(float _rumbleTime)
        {
            // TODO : togliere la vibrazione durante il count down (da fare nel refactoring dell'avatar)
            if(!GameManager.Instance.LevelMng.IsGamePaused)
                owner.player.ControllerVibration(owner.PlayerIndex, 0.5f, 0.5f);

            yield return new WaitForSeconds(_rumbleTime);
            owner.player.ControllerVibration(owner.PlayerIndex, 0f, 0f);
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
}
