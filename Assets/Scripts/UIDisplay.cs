using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {

    public Slider PlayerSlider;

    
    public void SetSliderValue(float _life)
    {
        PlayerSlider.value = _life / 10;
    }
}
