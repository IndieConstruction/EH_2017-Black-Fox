using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReferencesToGM : MonoBehaviour {

    List<Slider> SliderInChildren;
    Image WindDislay;
    Text textToWin;
    UIManager uiManager;

    public Slider SliderPlayer1;
    public Slider SliderPlayer2;
    public Slider SliderPlayer3;
    public Slider SliderPlayer4;
    public Slider CoreSlider;
    public Image WindDisplay;
    public Text TextWindDisplay;

    // Use this for initialization
    void Awake ()
    {
        uiManager = GameManager.Instance.GetUIManager();
        SetUIManager();
    }
	
    public void SetUIManager()
    {
        uiManager.SliderPlayer1 = SliderPlayer1;
        uiManager.SliderPlayer2 = SliderPlayer2;
        uiManager.SliderPlayer3 = SliderPlayer3;
        uiManager.SliderPlayer4 = SliderPlayer4;
        uiManager.CoreSlider = CoreSlider;
        uiManager.WindDisplay = WindDisplay;
        uiManager.TextWindDisplay = TextWindDisplay;
    }

    /*
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

    */

}