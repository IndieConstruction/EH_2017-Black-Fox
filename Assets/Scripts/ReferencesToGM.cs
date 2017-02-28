using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferencesToGM : MonoBehaviour {

    Slider[] SliderInChildren;

    private void Start()
    {
        FindComponentsInChildren();
    }

    
    void FindComponentsInChildren()
    {
        SliderInChildren = GetComponentsInChildren<Slider>();
        foreach (var item in SliderInChildren)
        {
            if (item.name == "SliderP1")
            {
                GameManager.Instance.uiManager.SliderPlayer1 = item;
            }
            if (item.name == "SliderP2")
            {
                GameManager.Instance.uiManager.SliderPlayer2 = item;
            }
            if (item.name == "SliderP3")
            {
                GameManager.Instance.uiManager.SliderPlayer3 = item;
            }
            if (item.name == "SliderP4")
            {
                GameManager.Instance.uiManager.SliderPlayer4 = item;
            }
            if (item.name == "SliderCore")
            {
                GameManager.Instance.uiManager.CoreSlider = item;
            }
        }
    }
    
}