using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        #region Prefabs
        public GameObject LevelManagerPrefab;
        public GameObject UIManagerPrefab;
        public GameObject PlayerManagerPrefab;
        public GameObject CoinManagerPrefab;
        public GameObject AudioManagerPrefab;
        public GameObject UpgradePointsManagerPrefab;
        public GameObject PowerUpManagerPrefab;
        public GameObject DataManagerPrefab;
        public GameObject SRManagerPrefab;
        #endregion

        #region Managers
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
        public UpgradePointsManager UpgradePointsMng;
        [HideInInspector]
        public PowerUpManager PowerUpManager;
        [HideInInspector]
        public DataManager DataMng;
        [HideInInspector]
        public SRManager SRMng;
        #endregion

        [HideInInspector]
        public FlowSM flowSM;

        public Level LevelScriptableObj;

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

        public void InstantiateUpgradePointsManager()
        {
            UpgradePointsMng = Instantiate(UpgradePointsManagerPrefab, transform).GetComponent<UpgradePointsManager>();
        }

        public void InstantiatePowerUpManager()
        {
            PowerUpManager = Instantiate(PowerUpManagerPrefab, transform).GetComponent<PowerUpManager>();
        }

        public void InstantiateDataManager()
        {
            DataMng = Instantiate(DataManagerPrefab, transform).GetComponent<DataManager>();
        }

        public void InstantiateShowRoom()
        {
            SRMng = Instantiate(SRManagerPrefab, transform).GetComponent<SRManager>();
        }
        #endregion
        #endregion
    }
}


