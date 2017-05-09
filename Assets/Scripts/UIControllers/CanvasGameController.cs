using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class CanvasGameController : MonoBehaviour
    {
        public GameUIController gameUIController;
        public EndRoundlUI endRoundUI;
        public PauseMenuController pauseMenuController;
        public Counter Counter;

        #region API
        /// <summary>
        /// Richiama la funzione per visualizzare il numero del livello e del round 
        /// </summary>
        public void UpdateLevelInformation()
        {
            gameUIController.UpdateLevelInformation();
        }
        #endregion

        private void OnEnable()
        {
            EventManager.OnAmmoValueChange += gameUIController.SetBulletsValue;
        }

        private void OnDestroy()
        {
            EventManager.OnAmmoValueChange -= gameUIController.SetBulletsValue;
        }
    }
}