using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class Player : MonoBehaviour {

    [SerializeField]
    PlayerID playerID;

    public float Speed;
    public float RotationSpeed;


    public float life;
    public int points;

    MovementController movment;
    PlacePin pinPlacer;

    int pinSide = 1; // 1 destra | -1 sinistra

    public float Life
    {
        get;
        set;
    }

    public float Points
    {
        get;
        set;
    }

    void Start ()
    {
        movment = GetComponent<MovementController>();
        pinPlacer = GetComponent<PlacePin>();
    }

    void Update()
    {
        ActionReader();
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            pinPlacer.ChangePinSpawnPosition(-1);
            pinPlacer.placeThePin();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            pinPlacer.ChangePinSpawnPosition(1);
            pinPlacer.placeThePin();
        }
        */
    }
    #region Controller Input
    void ActionReader()
    {
        if (InputManager.GetButtonDown("ChangeSide"))
        {
            if (pinSide == -1)
            {
                pinSide = 1;
            }               
            else if (pinSide == 1)
            {
                pinSide = -1;
            }
            pinPlacer.ChangePinSpawnPosition(pinSide);
        }

        if (InputManager.GetButtonDown("PlacePin"))
        {
            pinPlacer.placeThePin();
        }

        if (InputManager.GetAxis("LeftTrigger") >= 0.8f)
        {
            //Shoot
        }
        movment.Movement(InputManager.GetAxis("RightTrigger", playerID));
        movment.RotationTowards(new Vector3(InputManager.GetAxis("Horizontal", playerID), 0f, InputManager.GetAxis("Vertical", playerID)));
    }
    #endregion
}
