using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool dontDestroyOnLoad;
    //Variabile che contiene il valore della vita del core
    public float CoreLife;
    public bool CoreIsAlive;
    SceneController sceneControlle;

    public static GameManager Instance;


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
        sceneControlle = FindObjectOfType<SceneController>();
    }

    // Use this for initialization
    void Start () {
        //core = FindObjectOfType<Core>();
        
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sceneControlle.ReloadCurrentRound();
        }
    }




}
