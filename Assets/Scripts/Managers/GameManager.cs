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

        FlowSM flowSM;

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

    }
}

