using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;

public class Player : MonoBehaviour {

    [SerializeField]
    PlayerID playerID;

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
        if (InputManager.GetButtonDown("Button X")) // change side of pin
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

        if (InputManager.GetButtonDown("Button A")) // place pin
        {
            pinPlacer.placeThePin();
        }

        if (InputManager.GetAxis("Left Trigger") >= 0.8f) // shoot
        {
            //Shoot
        }
        float thrust = InputManager.GetAxis("Right Trigger", playerID); // add thrust
        Vector3 faceDirection = new Vector3(InputManager.GetAxis("Left Stick Horizontal", playerID), 0f, InputManager.GetAxis("Left Stick Vertical", playerID)); // rotate
        movment.Movement(thrust); 
        movment.RotationTowards(faceDirection); 
    }
    #endregion
}
