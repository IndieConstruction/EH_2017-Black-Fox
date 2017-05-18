using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class UIManager : MonoBehaviour
    {
        [HideInInspector]
        public MainMenuController canvasMenu;
        [HideInInspector]
        public LevelSelectionController canvasLevelSelection;
        [HideInInspector]
        public CanvasGameController canvasGame;
        [HideInInspector]
        public AvatarSelectionManager avatarSelectionManager;

        [HideInInspector]
        public LoadingScreen loadingCanvas;

        public GameObject AvatarUI;

        [HideInInspector]
        public BaseMenu CurrentMenu;

        #region API
        #region Menu Controller

        public void GoUpInMenu(Player _player)
        {
            if (GameManager.Instance.AudioMng != null)
                GameManager.Instance.AudioMng.PlayMenuMovmentAudio();
            CurrentMenu.GoUpInMenu(_player);
        }

        public void GoDownInMenu(Player _player)
        {
            if (GameManager.Instance.AudioMng != null)
                GameManager.Instance.AudioMng.PlayMenuMovmentAudio();
            CurrentMenu.GoDownInMenu(_player);
        }

        public void GoLeftInMenu(Player _player)
        {
            if (GameManager.Instance.AudioMng != null)
                GameManager.Instance.AudioMng.PlayMenuMovmentAudio();
            CurrentMenu.GoLeftInMenu(_player);
        }

        public void GoRightInMenu(Player _player)
        {
            if (GameManager.Instance.AudioMng != null)
                GameManager.Instance.AudioMng.PlayMenuMovmentAudio();
            CurrentMenu.GoRightInMenu(_player);
        }

        public void GoBackInMenu()
        {
            // TODO : implementare
        }

        public void SelectInMenu(Player _player)
        {
            if (GameManager.Instance.AudioMng != null)
                GameManager.Instance.AudioMng.PlayMenuSelectionAudio();
            CurrentMenu.Selection(_player);
        }

        #endregion

        #region Loading Canvas
        /// <summary>
        /// Crea il CanvasMenu non appena subentra il MainMenuState
        /// </summary>
        public void CreateLoadingCanvas()
        {
            loadingCanvas = Instantiate(Resources.Load("Prefabs/UI/LoadingCanvas") as GameObject, transform).GetComponentInChildren<LoadingScreen>();
        }

        /// <summary>
        /// Distrugge il CanvasMenu non appena subentra il MainMenuState
        /// </summary
        public void DestroyLoadingCanvas()
        {
            Destroy(loadingCanvas.gameObject);
        }
        #endregion

        #region Main Menu
        /// <summary>
        /// Crea il CanvasMenu non appena subentra il MainMenuState
        /// </summary>
        public void CreateMainMenu()
        {
            canvasMenu = Instantiate(Resources.Load("Prefabs/UI/CanvasMenu") as GameObject, transform).GetComponent<MainMenuController>();
        }

        /// <summary>
        /// Distrugge il CanvasMenu non appena subentra il MainMenuState
        /// </summary>
        public void DestroyMainMenu()
        {
            Destroy(canvasMenu.gameObject);
        }
        #endregion

        #region LevelSelection Menu
        /// <summary>
        /// Crea il CanvasLevelSelection non appena subentra il MainMenuState
        /// </summary>
        public void CreateLevelSelectionMenu()
        {
            canvasLevelSelection = Instantiate(Resources.Load("Prefabs/UI/CanvasLevelSelection") as GameObject, transform).GetComponent<LevelSelectionController>();
        }

        /// <summary>
        /// Distrugge il CanvasLevelSelection non appena subentra il MainMenuState
        /// </summary>
        public void DestroyLevelSelectionMenu()
        {
            Destroy(canvasLevelSelection.gameObject);
        }
        #endregion

        #region AvatarSelection Menu

        public void CreateAvatarSelectionMenu() {
            avatarSelectionManager = Instantiate(Resources.Load("Prefabs/UI/AvatarSelectionCanvas") as GameObject, transform).GetComponent<AvatarSelectionManager>();
        }

        /// <summary>
        /// Distrugge il CanvasMenu non appena subentra il MainMenuState
        /// </summary>
        public void DestroyAvatarSelectionMenu() {
            //Destroy(avatarSelectionManager.gameObject);
        }
        #endregion

        #region Game Menu
        /// <summary>
        /// Crea il Canvas Game Menu
        /// </summary>
        public void CreateGameMenu()
        {
            canvasGame = Instantiate(Resources.Load("Prefabs/UI/CanvasGame") as GameObject, transform).GetComponent<CanvasGameController>();
        }

        /// <summary>
        /// Distrugge il Canvas Game Menu
        /// </summary>
        public void DestroyGameMenu()
        {
            Destroy(canvasGame.gameObject);
        }
        #endregion

        #region AvatarUI

        /// <summary>
        /// Crea l'avatarUI
        /// </summary>
        /// <param name="_target">l'oggetto a cui attaccare la UI</param>
        public AvatarUI CreateAvatarUI(GameObject _target)
        {
            return Instantiate(AvatarUI, _target.transform.position, _target.transform.rotation, _target.transform).GetComponentInChildren<AvatarUI>();
        }

        #endregion

        #endregion
    }
}