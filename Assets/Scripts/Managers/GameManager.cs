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

        public int KillPoint;
        public int DeathPoint;
        public int PointsToWin;
        public bool dontDestroyOnLoad;
        public float AgentRespawnTime = 3f;

        SceneController sceneController;
        PointsManager pointsManager;
        RespawnAgent respawnAgent;
        UIManager managerUI;
        public Button TestSceneButton;
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
            if (Instance == null) { 
                Instance = this;
                //For actual debug pourpose
                if (dontDestroyOnLoad)
                    DontDestroyOnLoad(gameObject);
            } else { 
                DestroyImmediate(gameObject);
            }

            TestSceneButton = FindObjectOfType<Button>();
            sceneController = FindObjectOfType<SceneController>();
        }

        void Start()
        {
            managerUI = GetComponent<UIManager>();
            pointsManager = new PointsManager(KillPoint, DeathPoint, PointsToWin);
            respawnAgent = GetComponent<RespawnAgent>();
            flowSM = gameObject.AddComponent<FlowSM>();
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

        #region PointsManager
        public void SetKillPoints(PlayerIndex _killer, PlayerIndex _victim)
        {
            pointsManager.UpdateKillPoints(_killer, _victim);           // setta i punti morte e uccisione
            StartCoroutine("WaitForRespawn", _victim);                  // repawn dell'agente ucciso
        }

        public void SetKillPoints(PlayerIndex _victim)
        {
            pointsManager.UpdateKillPoints(_victim);           // setta i punti morte e uccisione
            StartCoroutine("WaitForRespawn", _victim);                  // repawn dell'agente ucciso
        }
        #endregion

        #region RespawnAgent
        public void SetAgentSpawnPoint(PlayerIndex _playerIndex, Transform _spawnpoint) {
            if (respawnAgent != null)
                respawnAgent.SetSpawnPoint(_playerIndex, _spawnpoint);
        }

        IEnumerator WaitForRespawn(PlayerIndex _victim)
        {
            yield return new WaitForSeconds(AgentRespawnTime);
            respawnAgent.Respawn(_victim);
        }
        #endregion
    }
}

