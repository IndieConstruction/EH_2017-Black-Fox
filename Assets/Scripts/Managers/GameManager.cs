using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameObject LevelManagerPrefab;
        public GameObject UIManagerPrefab;
        public GameObject PlayerManagerPrefab;
        public GameObject CoinManagerPrefab;
        public GameObject AudioManagerPrefab;

        public Level LevelScriptableObj;

        [HideInInspector]
        public LevelManager LevelMng;
        [HideInInspector]
        public UIManager UiMng;
        [HideInInspector]
        public PlayerManager PlayerMng;
        [HideInInspector]
        public CoinManager CoinMng;
        [HideInInspector]
        public AudioManager AudioMng;
        [HideInInspector]
        public FlowSM flowSM;

        private void Awake()
        {
            //Singleton paradigm
            if (Instance == null)
                Instance = this;
            else
                DestroyImmediate(gameObject);
        }

        void Start()
        {
            flowSM = gameObject.AddComponent<FlowSM>();
        }

        private void Update()
        {
            // TODO : togliere (Luca dice che è da togliere)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitApplication();
            }
        }

        #region API
        public void QuitApplication()
        {
            Application.Quit();
        }

        #region Instantiate Managers
        public void InstantiateLevelManager()
        {
            LevelMng = Instantiate(LevelManagerPrefab, transform).GetComponent<LevelManager>();
        }
        public void DestroyLevelManager()
        {
            if(LevelMng)
                Destroy(LevelMng.gameObject);
        }

        public void InstantiateCoinManager()
        {
            CoinMng = Instantiate(CoinManagerPrefab, transform).GetComponent<CoinManager>();
        }

        public void InstantiateUIManager()
        {
            UiMng = Instantiate(UIManagerPrefab, transform).GetComponent<UIManager>();
        }

        public void InstantiatePlayerManager()
        {
            PlayerMng = Instantiate(PlayerManagerPrefab, transform).GetComponent<PlayerManager>();
        }

        public void InstantiateAudioManager()
        {
            AudioMng = Instantiate(AudioManagerPrefab, transform).GetComponent<AudioManager>();
        }
        #endregion
        #endregion
    }
}

