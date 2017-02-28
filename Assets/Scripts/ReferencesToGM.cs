using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferencesToGM : MonoBehaviour {

    Slider[] SliderInChildren;
    public Image windDisplay;
    public Text textofWin;
    UIManager uiManager;

    private void Start()
    {
        uiManager = GameManager.Instance.GetUIManager();
        FindComponentsInChildren();
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

        uiManager.WindDisplay = windDisplay;

        uiManager.TextWindDisplay = textofWin;
    }
}