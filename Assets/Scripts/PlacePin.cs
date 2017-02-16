using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePin : MonoBehaviour {

    public GameObject stuffsprefab;
    public Transform PinSpanw;

    bool isLeft = false;

    public void placeThePin()
    {
        Instantiate(stuffsprefab, PinSpanw.position, PinSpanw.rotation);        
    }

    /// <summary>
    /// Change the position of the PinSpawnPoint
    /// </summary>
    /// <param name="_direction">Set the side of the PinSpawnPoint(1 Right; -1 Left)</param>
    public void ChangePinSpawnPosition(string _side)
    {
        float tranformX = 0f;
        if (_side == "Left")
        {
            tranformX = -600f;
        }
        else if (_side == "Right")
        {
            tranformX = 600f;
        }

        PinSpanw.localPosition = new Vector3(tranformX, PinSpanw.localPosition.y, PinSpanw.localPosition.z);
    } 
}
