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
    Shoot shoot;

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
        shoot = GetComponent<Shoot>();
    }

    void Update()
    {
        
        ActionReader();
        
    }


    #region Controller Input
    void ActionReader()
    {
        if (InputManager.GetButtonDown("Right Bumper", playerID)) // place pin
        {
            pinPlacer.ChangePinSpawnPosition(600);
            pinPlacer.placeThePin();
        }

        if (InputManager.GetButtonDown("Left Bumper", playerID)) // place pin
        {
            pinPlacer.ChangePinSpawnPosition(-600);
            pinPlacer.placeThePin();
        }

        if (InputManager.GetButtonDown("Button A", playerID)) // shoot
        {
            shoot.ShootBullet(100);
        }
        float thrust = InputManager.GetAxis("Right Trigger", playerID); // add thrust
        Vector3 faceDirection = new Vector3(InputManager.GetAxis("Left Stick Horizontal", playerID), 0f, InputManager.GetAxis("Left Stick Vertical", playerID)); // rotate
        movment.Movement(thrust); 
        movment.RotationTowards(faceDirection); 
    }



    void CheckAxis(string _axis, float _parameter)
    {
        bool Read = false;

        //La variabile partendo falsa, permette di entrare nel ciclo if.
        if (Read == false)
        {
            if (Input.GetAxisRaw(_axis) >= _parameter)
            {
                //una volta entrata la prima volta la variabile read ritorna vera ed esce dal primo if.
                Read = true;
                Debug.Log("Destra");
            }
        }
        if (Input.GetAxisRaw(_axis) <= 0.15f)
            Read = false;

    }


    #endregion
}
