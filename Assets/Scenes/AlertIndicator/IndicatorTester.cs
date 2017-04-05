using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorTester : MonoBehaviour {

    public Transform transformToIndicate;
    public Image Indicator;
    public Image Indicator2;
    public BoxCollider CameraCollider;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Indicator.transform.position = Camera.main.WorldToScreenPoint(transformToIndicate.position);
        CameraCollider.size = new Vector3(Screen.width, Screen.height, 100);
        Indicator2.transform.position = Camera.main.WorldToViewportPoint(transformToIndicate.position);
    }
}
