using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool dontDestroyOnLoad;
    //Variabile che contiene il valore della vita del core
    public float CoreLife;
    public bool CoreIsAlive;
    public SceneController sceneController;
    List<AvatarParameters> avatarParameters = new List<AvatarParameters>();

    
    


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
        } else {
            Destroy(gameObject);
        }
        
        sceneController = FindObjectOfType<SceneController>();
    }

    // Use this for initialization
    void Start () {
        //core = FindObjectOfType<Core>();
        
	}

   


    public void AddPlayer(PlayerID _IDPlayer, string _name, float _life, float _powerPoint)
    {
        avatarParameters.Add(new AvatarParameters(_IDPlayer, _name, _life, _powerPoint));
    }



    public void SetPlayerLife(PlayerID _IDPlayer, float _life)
    {
        foreach (var item in avatarParameters)
        {
            if (item.playerID == _IDPlayer)
            {
                item.Life = _life;
            }
        }
    }

    public void SetPlayerPowerPoint(PlayerID _IDPlayer, float _powerPoint)
    {
        foreach (var item in avatarParameters)
        {
            if (item.playerID == _IDPlayer)
            {
                item.PowerPoint = _powerPoint;
            }
        }
    }

}
