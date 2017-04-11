using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
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
            canvasMenu = GameObject.Instantiate(Resources.Load("Prefabs/UI/CanvasMenu"), transform);
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
            canvasLevelSelection = GameObject.Instantiate(Resources.Load("Prefabs/UI/CanvasLevelSelection"), transform);
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
            canvasGameMenu = GameObject.Instantiate(Resources.Load("Prefabs/UI/Canvas"), transform);
            endRoundUI = GetComponentInChildren<EndRoundlUI>();
            gameUIController = GetComponentInChildren<GameUIController>();
        }

        /// <summary>
        /// Distrugge il Canvas contenente il GameUIController e l'EndRoundUI
        /// </summary>
        public void DestroyGameMenu()
        {
            Destroy(canvasGameMenu);
        }
        #endregion

        #endregion     
    }
}