using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;
using System;

public class Player : MonoBehaviour, IShooter, IDamageable {

    [SerializeField]
    PlayerID playerID;

    public float life;
    public float points;

    public List<GameObject> DamageablesPrefabs = new List<GameObject>();
    List<IDamageable> Damageables = new List<IDamageable>();

    MovementController movment;
    PlacePin pinPlacer;
    Shoot shoot;

    bool isAlive = true;

    public float Life
    {
        get { return life; }
        set
        {
            if (isAlive)
                life = value;
        }
    }

    public float Points
    {
        get { return points; }
        set { points = value; }
    }

    void Start ()
    {
        movment = GetComponent<MovementController>();
        pinPlacer = GetComponent<PlacePin>();
        shoot = GetComponent<Shoot>();

        LoadIDamageablePrefab();
    }

    void Update()
    {
        ActionReader();
        ChechLife();
    }

    private void ChechLife()
    {
        if(Life < 1)
        {
            isAlive = false;
            Debug.Log("Sei Morto n# " + playerID);
            gameObject.SetActive(false);
        } 
    }

    private void LoadIDamageablePrefab()
    {
        foreach (var k in DamageablesPrefabs)
        {
            if (k.GetComponent<IDamageable>() != null)
                Damageables.Add(k.GetComponent<IDamageable>());
        }
    }

    #region Controller Input
    void ActionReader()
    {
        if (InputManager.GetButtonDown("Right Bumper", playerID)) // place right pin
        {
            pinPlacer.ChangePinSpawnPosition("Right");
            pinPlacer.placeThePin();
        }

        if (InputManager.GetButtonDown("Left Bumper", playerID)) // place left pin
        {
            pinPlacer.ChangePinSpawnPosition("Left");
            pinPlacer.placeThePin();
        }

        if (InputManager.GetButtonDown("Button A", playerID)) // shoot
        {
            shoot.ShootBullet();
        }
        float thrust = InputManager.GetAxis("Right Trigger", playerID); // add thrust
        Vector3 faceDirection = new Vector3(InputManager.GetAxis("Left Stick Horizontal", playerID), 0f, InputManager.GetAxis("Left Stick Vertical", playerID)); // rotate
        movment.Movement(-thrust); // il grilletto destro ritorna un valore negativo
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

    #region Interfaces

    #region IDamageable
    public void Damage(float _damage)
    {
        Life -= _damage;
        Debug.Log("Damage : " + _damage + " # Life : " + Life);
    }
    #endregion

    #region IShooter
    public List<IDamageable> GetDamageable()
    {
        return Damageables;
    }

    public GameObject GetOwner()
    {
        return gameObject;
    }
    #endregion

    #endregion
}
