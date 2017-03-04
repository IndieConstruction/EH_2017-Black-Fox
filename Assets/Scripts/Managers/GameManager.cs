using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public int KillPoint;
    public int DeathPoint;
    public int PointsToWin;
    public bool dontDestroyOnLoad;
    public float AgentRespawnTime = 3f;

    SceneController sceneController;
    PointsManager pointsManager;
    RespawnAgent respawnAgent;
    GameUIController gameUI;

    float coreLife;                 // vita del Core

    public float CoreLife
    {
        get { return coreLife; }
        set { coreLife = value; }
    }

    private void Awake()
    {
        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(gameObject);
        }

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        sceneController = FindObjectOfType<SceneController>();
    }

    void Start ()
    {
        pointsManager = new PointsManager(KillPoint, DeathPoint, PointsToWin);
        respawnAgent = GetComponent<RespawnAgent>();
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #region GameUIController
    public void SetGameUIController(GameUIController _gameUI)
    {
        gameUI = _gameUI;
    }

    public GameUIController GetGameUIController()
    {
        return gameUI;
    }


    public void SliderValueUpdate(PlayerIndex _playerIndex, float _life)
    {
        gameUI.SetSliderValue(_playerIndex, _life);
    }

    public void BullletsValueUpdate(PlayerIndex _playerIndex, int _remainigAmmo)
    {
        gameUI.SetBulletsValue(_playerIndex, _remainigAmmo);
    }

    public void CoreSliderValueUpdate(float _life, float _maxLife)
    {
        gameUI.SetCoreSliderValue(_life, _maxLife);
    }

    public  void ElementZeroValueUpdate(float _life, float _maxLife)
    {
        gameUI.SetElementZeroSlider(_life, _maxLife);
    }

    public void DisplayWinnerPlayer(PlayerIndex _playerIndex)
    {
        gameUI.ShowWinner(_playerIndex);
    }
    #endregion

    #region SceneController
    public void ChangeScene()
    {
        sceneController.LoadScene(1, false);
    }

    public void ReloadScene()
    {
        sceneController.ReloadCurrentRound();
    }
    #endregion

    #region PointsManager
    public void SetKillPoints(PlayerIndex _killer, PlayerIndex _victim)
    {
        pointsManager.UpdateKillPoints(_killer, _victim);           // setta i punti morte e uccisione
        StartCoroutine("WaitForRespawn", _victim);                  // repawn dell'agente ucciso
    }
    #endregion

    #region RespawnAgent
    public void SetAgentSpawnPoint(PlayerIndex _playerIndex, Transform _spawnpoint)
    {
        respawnAgent.SetSpawnPoint(_playerIndex, _spawnpoint);
    }

    IEnumerator WaitForRespawn(PlayerIndex _victim)
    {
        yield return new WaitForSeconds(AgentRespawnTime);   
        respawnAgent.Respawn(_victim);
    }
    #endregion
}

