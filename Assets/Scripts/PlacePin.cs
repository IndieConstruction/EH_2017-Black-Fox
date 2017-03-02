﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePin : MonoBehaviour {

    public GameObject PinPrefab;
    public Transform PinSpanw;
    public float CoolDownTime;

    public bool CanPlace = true;//TODO: rimpiazza questa variabile semplicemente facendo disabilitare il componente
    bool isLeft = false;
    
    float xValue;
    float prectime;

    private void Start()
    {
        xValue = PinSpanw.localPosition.x;
        prectime = CoolDownTime;
    }

    private void Update()
    {
        prectime -= Time.deltaTime;
    }

    /// <summary>
    /// Instantiate the pin on the PinSpawn
    /// </summary>
    public void placeThePin(Agent _owner)
    {
        ChangePinSpawnPosition();
        if (prectime <= 0 && CanPlace == true)
        {
            Instantiate(PinPrefab, PinSpanw.position, PinSpanw.rotation);
            _owner.AddShooterAmmo();
            prectime = CoolDownTime;
        }
    }

    /// <summary>
    /// Change the position of the PinSpawnPoint
    /// </summary>
    /// <param name="_direction">Set the side of the PinSpawnPoint</param>
    void ChangePinSpawnPosition()
    {
        if (!isLeft)
        {
            isLeft = true;
            xValue = -xValue;
        }
            
        else if (isLeft)
        {
            isLeft = false;
            xValue = -xValue;
        }
        PinSpanw.localPosition = new Vector3(xValue, PinSpanw.localPosition.y, PinSpanw.localPosition.z);
    } 
}
