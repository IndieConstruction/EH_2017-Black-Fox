using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    // TODO : creare un controller separato per il canvas game invece di delegare la funzione allo ui manager
    public class UIManager : MonoBehaviour
    {
        [HideInInspector]
        public EndRoundlUI endRoundUI;
        [HideInInspector]
        public GameUIController gameUIController;
        [HideInInspector]
        public Object canvasMenu;
        [HideInInspector]
        public Object canvasLevelSelection;
        [HideInInspector]
        public Object canvasGameMenu;
        [HideInInspector]
        public Counter Counter;
        [HideInInspector]
        public PauseMenuController pauseMenuController;

        public IMenu CurrentMenu;

        #region API

        /// <summary>
        /// Richiama la funzione per visualizzare il numero del livello e del round 
        /// </summary>
        public void UpdateLevelInformation()
        {
            gameUIController.UpdateLevelInformation();
        }

        #region Menu Controller

        public void GoUpInMenu()
        {
            if(GameManager.Instance.AudioMng != null)
                GameManager.Instance.AudioMng.PlayMenuMovmentAudio();
            CurrentMenu.GoUpInMenu();
        }

        public void GoDownInMenu()
        {
            if (GameManager.Instance.AudioMng != null)
                GameManager.Instance.AudioMng.PlayMenuMovmentAudio();
            CurrentMenu.GoDownInMenu();
        }

        public void GoBackInMenu()
        {
            // TODO : implementare
        }

        public void SelectInMenu()
        {
            if (GameManager.Instance.AudioMng != null)
                GameManager.Instance.AudioMng.PlayMenuSelectionAudio();
            CurrentMenu.Selection();
        }

        #endregion

        #region Main Menu
        /// <summary>
        /// Crea il CanvasMenu non appena subentra il MainMenuState
        /// </summary>
        public void CreateMainMenu()
        {
            canvasMenu = Instantiate(Resources.Load("Prefabs/UI/CanvasMenu"), transform);
        }

        /// <summary>
        /// Distrugge il CanvasMenu non appena subentra il MainMenuState
        /// </summary>
        public void DestroyMainMenu()
        {
            Destroy(canvasMenu);
        }
        #endregion

        #region LevelSelection Menu
        /// <summary>
        /// Crea il CanvasLevelSelection non appena subentra il MainMenuState
        /// </summary>
        public void CreateLevelSelectionMenu()
        {
            canvasLevelSelection = Instantiate(Resources.Load("Prefabs/UI/CanvasLevelSelection"), transform);
        }

        /// <summary>
        /// Distrugge il CanvasLevelSelection non appena subentra il MainMenuState
        /// </summary>
        public void DestroyLevelSelectionMenu()
        {
            Destroy(canvasLevelSelection);
        }
        #endregion

        #region Game Menu

        /// <summary>
        /// Crea il Canvas contenente il GameUIController e l'EndRoundUI
        /// </summary>
        public void CreateGameMenu()
        {
            canvasGameMenu = Instantiate(Resources.Load("Prefabs/UI/CanvasGame"), transform);
            endRoundUI = GetComponentInChildren<EndRoundlUI>();
            pauseMenuController = GetComponentInChildren<PauseMenuController>();
            gameUIController = GetComponentInChildren<GameUIController>();
            Counter = GetComponentInChildren<Counter>();
            EventManager.OnAmmoValueChanged += gameUIController.SetBulletsValue;
        }

        /// <summary>
        /// Distrugge il Canvas contenente il GameUIController e l'EndRoundUI
        /// </summary>
        public void DestroyGameMenu()
        {
            EventManager.OnAmmoValueChanged -= gameUIController.SetBulletsValue;
            Destroy(canvasGameMenu);
        }
        #endregion

        #endregion


        #region Events

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }

        #endregion

    }
}