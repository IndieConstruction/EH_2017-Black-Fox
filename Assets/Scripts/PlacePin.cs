using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePin : MonoBehaviour {

    public GameObject PinPrefab;
    public Transform PinSpanw;
    public float CoolDownTime;

    Agent owner;

    bool isLeft = false;
    
    float xPosValue;
    float xNegValue;
    float xValue;
    float prectime =-5.0f;
    private void Start()
    {
        float pinXvalue = PinSpanw.localPosition.x;
        xPosValue = xValue = pinXvalue;
        xNegValue = -pinXvalue;
    }


    /// <summary>
    /// Instantiate the pin on the PinSpawn
    /// </summary>
    public void placeThePin(Agent _owner)
    {
        if (Time.time >= prectime + CoolDownTime)
        {
            Instantiate(PinPrefab, PinSpanw.position, PinSpanw.rotation);
            _owner.AddAmmo();
            prectime = Time.time;
        }
    }

    /// <summary>
    /// Change the position of the PinSpawnPoint
    /// </summary>
    /// <param name="_direction">Set the side of the PinSpawnPoint</param>
    public void ChangePinSpawnPosition(string _side)
    {
        if (_side == "Left" && !isLeft)
        {
            xValue = xNegValue;
            isLeft = true;
        }
        else if (_side == "Right" && isLeft)
        {
            xValue = xPosValue;
            isLeft = false;
        }

        PinSpanw.localPosition = new Vector3(xValue, PinSpanw.localPosition.y, PinSpanw.localPosition.z);
    } 
}
