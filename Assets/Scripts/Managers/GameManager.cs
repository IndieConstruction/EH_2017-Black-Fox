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
    //Variabile che contiene il valore della vita del core
    public float coreLife = 10;                 // La vita del Core
    public float MaxLifeCore = 10;              // La vita massima che può avere il Core e che viene impostata al riavvio di un round perso
    public float WaitForSeconds = 3;                // Il tempo che aspetta prima di riavviare la scena
    public float AgentRespawnTime = 3f;
    public SceneController sceneController;
    PointsManager pointsManager;
    RespawnAgent respawnAgent;
    public UIManager uiManager;

    
    public float CoreLife
    {
        get
        {
            return coreLife;
        }
        set
        {
            coreLife = value;
        }
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
        } else {
            Destroy(gameObject);
        }
        
        sceneController = FindObjectOfType<SceneController>();
        
    }


    void Start ()
    {
        uiManager = GetComponent<UIManager>();
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

    public void SliderValueUpdate(PlayerIndex _playerIndex, float _life)
    {
        uiManager.SetSliderValue(_playerIndex, _life);
    }

    public void ChangeScene()
    {
        sceneController.LoadScene(1);
        uiManager.GameCanvasState(true);
        uiManager.MenuCanvasState(false);
    }

    public void SetAgentSpawnPoint(PlayerIndex _playerIndex, Transform _spawnpoint)
    {
        respawnAgent.SetSpawnPoint(_playerIndex, _spawnpoint);
    }

    public void SetKillPoints(PlayerIndex _killer, PlayerIndex _victim)
    {
        pointsManager.UpdateKillPoints(_killer, _victim);           // setta i punti morte e uccisione
        StartCoroutine("WaitForRespawn", _victim);                  // repawn dell'agente ucciso
    }

    IEnumerator WaitForRespawn(PlayerIndex _victim)
    {
        yield return new WaitForSeconds(AgentRespawnTime);   
        respawnAgent.Respawn(_victim);
    }
}

