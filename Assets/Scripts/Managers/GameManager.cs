using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class GameManager : MonoBehaviour
    {
        int levelNumber;

        public int LevelNumber {
            get { return levelNumber; }
            set { levelNumber = value; }
        }

        public static GameManager Instance;

        public bool dontDestroyOnLoad;

        public GameObject LevelManagerPrefab;
        public GameObject UIManagerPrefab;

        [HideInInspector]
        public LevelManager levelManager;
        [HideInInspector]
        public UIManager uiManager;

        public FlowSM flowSM;

        private void Awake()
        {
            //Singleton paradigm
            if (Instance == null)
            { 
                Instance = this;
                //For actual debug pourpose
                if (dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            }
            else
            { 
                DestroyImmediate(gameObject);
            }
        }

        void Start()
        {
            flowSM = gameObject.AddComponent<FlowSM>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        #region API

        //public FlowSM ReturnFlowSM()
        //{
        //    return flowSM;
        //}

        public void InstantiateLevelManager()
        {
            levelManager = Instantiate(LevelManagerPrefab, transform).GetComponent<LevelManager>();
            // TODO : modificare assegnazione del level number
            levelManager.levelNumber = 1;
        }
        public void InstantiateUIManager()
        {
            uiManager = Instantiate(UIManagerPrefab, transform).GetComponent<UIManager>();
        }
        #endregion
    }
}

