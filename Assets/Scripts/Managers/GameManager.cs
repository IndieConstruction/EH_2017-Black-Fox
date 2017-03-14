using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

namespace BlackFox
{
    public class GameManager : MonoBehaviour
    {

        public static GameManager Instance;

        public bool dontDestroyOnLoad;

        UIManager managerUI;
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
            managerUI = GetComponent<UIManager>();
            flowSM = gameObject.AddComponent<FlowSM>();
            FlowSM flowSM2 = flowSM;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public UIManager GetUIManager()
        {
            return managerUI;
        }
    }
}

