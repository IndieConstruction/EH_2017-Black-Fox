using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class IndicatorTester : MonoBehaviour {

    public Transform transformToIndicate;
    public Image Indicator;

    private bool _offScreen;

    public bool OffScreen {
        get { return _offScreen; }
        set {
            _offScreen = value;
            Indicator.gameObject.SetActive(_offScreen);
        }
    }

    private void Start() {
        transformToIndicate = transform;
    }

    void Update () {
        float offset = 0;//Indicator.rectTransform.sizeDelta.x;
        OffScreen = false;
        Indicator.transform.position = Camera.main.WorldToScreenPoint(transformToIndicate.position);
        if (Indicator.transform.position.x > Screen.width) {
            Indicator.transform.position = new Vector2(Screen.width - offset, Indicator.transform.position.y);
            OffScreen = true;
        }
        if (Indicator.transform.position.x < 0) {
            Indicator.transform.position = new Vector2(0 + offset, Indicator.transform.position.y);
            OffScreen = true;
        }
        if (Indicator.transform.position.y > Screen.height) {
            Indicator.transform.position = new Vector2(Indicator.transform.position.x, Screen.height - offset);
            OffScreen = true;
        }
        if (Indicator.transform.position.y < 0) {
            Indicator.transform.position = new Vector2(Indicator.transform.position.x, 0 + offset);
            OffScreen = true;
        }


        
    }
}
