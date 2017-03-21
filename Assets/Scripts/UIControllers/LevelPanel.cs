using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour {

    public int levelNum;

    public int LevelNum {
        get { return levelNum; }
        set { levelNum = value; }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ConsoleDebug()
    {
        Debug.Log("Hai cliccato");
    }
}
