using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BlackFox;
using DG.Tweening;


public class AlertIndicator : MonoBehaviour {
    
    Image Indicator;
    Vector3 centerPosition { get { return GameManager.Instance.LevelMng.Core.transform.position; } }

    private bool _offScreen;

    public bool OffScreen {
        get { return _offScreen; }
        set {
            _offScreen = value;
            Indicator.gameObject.SetActive(_offScreen);
        }
    }

    private void Start()
    {
        Indicator = Instantiate(Resources.Load<GameObject>("Prefabs/UI/AlertIndicator")).GetComponent<Image>();
        Indicator.transform.SetParent(GameManager.Instance.UiMng.canvasGame.transform);
    }

    void Update ()
    {
        float offset = 0;//Indicator.rectTransform.sizeDelta.x;
        //OffScreen = false;
        Indicator.transform.position = Camera.main.WorldToScreenPoint(Vector3.ProjectOnPlane(transform.position,-Camera.main.transform.forward));
        if (Indicator.transform.position.x > Screen.width)
        {
            Indicator.transform.position = new Vector2(Screen.width - offset, Indicator.transform.position.y);
            OffScreen = true;
        }

        if (Indicator.transform.position.x < 0)
        {
            Indicator.transform.position = new Vector2(0 + offset, Indicator.transform.position.y);
            OffScreen = true;
        }

        if (Indicator.transform.position.y > Screen.height)
        {
            Indicator.transform.position = new Vector2(Indicator.transform.position.x, Screen.height - offset);
            OffScreen = true;
        }

        if (Indicator.transform.position.y < 0)
        {
            Indicator.transform.position = new Vector2(Indicator.transform.position.x, 0 + offset);
            OffScreen = true;
        }  
    }

    private void OnDestroy()
    {
        Destroy(Indicator.gameObject);
    }
}
