using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class UIManager : MonoBehaviour {

    public Slider SliderPlayer1;
    public Slider SliderPlayer2;
    public Slider SliderPlayer3;
    public Slider SliderPlayer4;
    public Slider CoreSlider;
    public Image WindDisplay;
    public Text TextWindDisplay;
    public Canvas CanvasMenu;
    public Canvas CanvasGame;

    public void SetSliderValue(PlayerIndex _playerIndex, float _life)
    {
        // TODO: Se sono presenti due Canvas ci saranno dei conflitti
        if (FindObjectOfType<Canvas>() != null)
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
    }

    public void SetCoreSliderValue(float _life)
    {
        if (FindObjectOfType<Canvas>() != null)
            CoreSlider.value = _life / 10;
    }

    public void MenuCanvasState(bool _value)
    {
        CanvasMenu.enabled = _value;
    }

    public void GameCanvasState(bool _value)
    {
        CanvasGame.enabled = _value;
    }
}
