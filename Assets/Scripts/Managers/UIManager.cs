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

        void Start()
        {
            endRoundUI = GetComponentInChildren<EndRoundlUI>();
            gameUIController = GetComponentInChildren<GameUIController>();
        }

        #region API
        #region Main Menu
        public void CreateMainMenu()
        {
            canvasMenu = GameObject.Instantiate(Resources.Load("Prefabs/UI/CanvasMenu"), transform);
        }

        public void DestroyMainMenu()
        {
            Destroy(canvasMenu);
        }
        #endregion
        #region LevelSelection Menu
        public void CreateLevelSelectionMenu()
        {
            canvasLevelSelection = GameObject.Instantiate(Resources.Load("Prefabs/UI/CanvasLevelSelection"), transform);
        }

        public void DestroyLevelSelectionMenu()
        {
            Destroy(canvasLevelSelection);
        }
        #endregion
        #region Game Menu
        public void CreateGameMenu()
        {
            canvasGameMenu = GameObject.Instantiate(Resources.Load("Prefabs/UI/Canvas"), transform);
        }

        public void DestroyGameMenu()
        {
            Destroy(canvasGameMenu);
        }
        #endregion
        #endregion

        #region Events

        
        protected void OnEnable()
        {
            EventManager.OnAgentKilled += CallAddKillPoinToUI;
        }

        protected void OnDisable()
        {
            EventManager.OnAgentKilled -= CallAddKillPoinToUI;
        }

        #endregion

        /// <summary>
        /// Richiama la funzione AddKillPointToUI presente nell'EndRoundUI
        /// </summary>
        /// <param name="_attacker"></param>
        /// <param name="_victim"></param>
        void CallAddKillPoinToUI(Agent _attacker, Agent _victim)
        {
            endRoundUI.AddKillPointToUI(_attacker, _victim);
        }
    }
}