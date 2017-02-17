using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePin : MonoBehaviour {

    public GameObject stuffsprefab;
    public Transform PinSpanw;

    bool isLeft = false;
    
    float xPosValue;
    float xNegValue;
    float xValue;

    private void Start()
    {
        float pinXvalue = PinSpanw.localPosition.x;
        xPosValue = xValue = pinXvalue;
        xNegValue = -pinXvalue;
    }


    /// <summary>
    /// Instantiate the pin on the PinSpawn
    /// </summary>
    public void placeThePin()
    {
        Instantiate(stuffsprefab, PinSpanw.position, PinSpanw.rotation);        
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
