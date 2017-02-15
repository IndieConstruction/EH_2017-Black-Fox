using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePin : MonoBehaviour {

    public GameObject stuffsprefab;
    public Transform PinSpanw;


    public void placeThePin()
    {
        Instantiate(stuffsprefab, PinSpanw.position, PinSpanw.rotation);
    }

    /// <summary>
    /// Change the position of the PinSpawnPoint
    /// </summary>
    /// <param name="_direction">Set the side of the PinSpawnPoint(1 Right; -1 Left)</param>
    public void ChangePinSpawnPosition(int _direction)
    {
        float TRanformX = 0f;
        if(_direction == 1)
        {
            TRanformX = PinSpanw.localPosition.x;
        }
        else if (_direction == -1)
        {
            TRanformX = -PinSpanw.localPosition.x;
        }

        PinSpanw.localPosition = new Vector3(TRanformX, PinSpanw.localPosition.y, PinSpanw.localPosition.z);
    } 
}
