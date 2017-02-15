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

    public PlayerID PlayerID
    {
        get { return playerID; }
    }
    
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
    }
    #region Controller Input
    void ActionReader()
    {
        if (InputManager.GetButtonDown("PlacePin"))
        {

        }
        if (InputManager.GetButtonDown("ChangeSide"))
        {

        }
        float horizontalAxis = InputManager.GetAxis("Horizontal", playerID);
        float verticalAxis = InputManager.GetAxis("Vertical", playerID);
        float forwardParameter = InputManager.GetAxis("RightTrigger", playerID);
        movment.Movement(forwardParameter);
        movment.RotationTowards(new Vector3(horizontalAxis, 0f, verticalAxis));
    }
    #endregion
}
