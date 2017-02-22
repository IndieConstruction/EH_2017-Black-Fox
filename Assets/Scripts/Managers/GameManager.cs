using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager Instance;

    public int KillPoint;
    public int DeathPoint;
    public int PointsToWin;

    public bool dontDestroyOnLoad;
    //Variabile che contiene il valore della vita del core
    public float CoreLife;
    public bool CoreIsAlive;
    public SceneController sceneController;
    PointsManager pointsManager;

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

    // Use this for initialization
    void Start () {
        //core = FindObjectOfType<Core>();
        pointsManager = new PointsManager(KillPoint, DeathPoint, PointsToWin);

    }

    public void SetKillPoints(PlayerID _killer, PlayerID _victim)
    {
        pointsManager.UpdateKillPoints(_killer, _victim);
    }
}
