using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class GameUIController : MonoBehaviour {

    public Slider SliderPlayer1;
    public Text Player1BulletCount;
    public Slider SliderPlayer2;
    public Text Player2BulletCount;
    public Slider SliderPlayer3;
    public Text Player3BulletCount;
    public Slider SliderPlayer4;
    public Text Player4BulletCount;
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
        else if (_playerIndex == PlayerIndex.Two)
        {
            SliderPlayer2.value = _life / 10;
        }
        else if (_playerIndex == PlayerIndex.Three)
        {
            SliderPlayer3.value = _life / 10;
        }
        else if (_playerIndex == PlayerIndex.Four)
        {
            SliderPlayer4.value = _life / 10;
        } 
    }

    public void SetBulletsValue(PlayerIndex _playerIndex, int _remainigAmmo)
    {
        if (_playerIndex == PlayerIndex.One)
        {
            Player1BulletCount.text = _remainigAmmo.ToString();
        }
        else if (_playerIndex == PlayerIndex.Two)
        {
            Player2BulletCount.text = _remainigAmmo.ToString();
        }
        else if (_playerIndex == PlayerIndex.Three)
        {
            Player3BulletCount.text = _remainigAmmo.ToString();
        }
        else if (_playerIndex == PlayerIndex.Four)
        {
            Player4BulletCount.text = _remainigAmmo.ToString();
        }
    }

    public void SetCoreSliderValue(float _life)
    {
        CoreSlider.value = _life / 10;
    }

    public void ShowWinner(PlayerIndex _playerIndex)
    {
        WindDisplay.gameObject.SetActive(true);
        TextWindDisplay.text = "Player" + _playerIndex + " Ha vinto! ";
    }

    public void CallReloadScene()
    {
        GameManager.Instance.ReloadScene();
    }
}
