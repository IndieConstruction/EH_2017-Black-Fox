using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferencesToGM : MonoBehaviour {

    Slider[] SliderInChildren;
    Image[] WindDislay;
    Text[] textToWin;
    UIManager uiManager;

    // Use this for initialization
    void Start () {
        uiManager = GameManager.Instance.GetUIManager();
        FindComponentsInChildren();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(SliderInChildren.Length);
        }
	}

    void FindComponentsInChildren()
    {
        SliderInChildren = GetComponentsInChildren<Slider>();
        foreach (var item in SliderInChildren)
        {
            if (item.name == "SliderP1")
            {
                uiManager.SliderPlayer1 = item;
            }
            if (item.name == "SliderP2")
            {
                uiManager.SliderPlayer2 = item;
            }
            if (item.name == "SliderP3")
            {
                uiManager.SliderPlayer3 = item;
            }
            if (item.name == "SliderP4")
            {
                uiManager.SliderPlayer4 = item;
            }
            if (item.name == "SliderCore")
            {
                uiManager.CoreSlider = item;
            }
        }

        WindDislay = GetComponentsInChildren<Image>();

        foreach (var item in WindDislay)
        {
            if (item.name == "WindDisplay")
            {
                uiManager.WindDisplay = item;
            }
        }

        textToWin = GetComponentsInChildren<Text>();
        foreach (var item in textToWin)
        {
            if (item.name == "TextToWin")
            {
                uiManager.TextWindDisplay = item;
            }
        }

    }

}