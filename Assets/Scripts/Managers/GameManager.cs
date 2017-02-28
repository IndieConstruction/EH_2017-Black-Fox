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
    UIManager uiManager;

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
        uiManager = GetComponent<UIManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    #region UIManager
    public void SliderValueUpdate(PlayerIndex _playerIndex, float _life)
    {
        uiManager.SetSliderValue(_playerIndex, _life);
    }

    public void CoreSliderValueUpdate(float _life)
    {
        uiManager.SetCoreSliderValue(_life);
    }

    public void DisplayWinnerPlayer(PlayerIndex _playerIndex)
    {
        uiManager.WindDisplay.gameObject.SetActive(true);
        uiManager.TextWindDisplay.text = "Player" + _playerIndex + " Ha vinto! ";
    }
    #endregion

    #region SceneController
    public void ChangeScene()
    {
        sceneController.LoadScene(1);
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

    /// <summary>
    /// permette ad un altra classe di salvarsi il riferimento allo UIManager tramite l'Instance del GameManager
    /// </summary>
    /// <returns></returns>
    public UIManager GetUIManager()
    {
        return uiManager;
    }
}

