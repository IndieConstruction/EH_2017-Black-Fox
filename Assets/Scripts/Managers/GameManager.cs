using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

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

        private float coreLife;
        public float CoreLife
        {
            get { return coreLife; }
            set { coreLife = value; }
        }

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

