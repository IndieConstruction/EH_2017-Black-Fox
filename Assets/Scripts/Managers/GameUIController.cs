using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class GameUIController : MonoBehaviour {

    public Slider SliderPlayer1;
    public Slider SliderPlayer2;
    public Slider SliderPlayer3;
    public Slider SliderPlayer4;
    public Slider CoreSlider;
    public Image WindDisplay;
    public Text TextWindDisplay;

    private void Awake()
    {
        GameManager.Instance.SetGameUIController(this);
    }

    public void SetSliderValue(PlayerIndex _playerIndex, float _life)
    {
        if (_playerIndex == PlayerIndex.One)
        {
            SliderPlayer1.value = _life / 10;
        }

        if (_playerIndex == PlayerIndex.Two)
        {
            SliderPlayer2.value = _life / 10;
        }

        if (_playerIndex == PlayerIndex.Three)
        {
            SliderPlayer3.value = _life / 10;
        }

        if (_playerIndex == PlayerIndex.Four)
        {
            SliderPlayer4.value = _life / 10;
        } 
    }

    public void SetCoreSliderValue(float _life)
    {
        if (FindObjectOfType<Canvas>() != null)
            CoreSlider.value = _life / 10;
    }

    public void CallReloadScene()
    {
        GameManager.Instance.ReloadScene();
    }
}
