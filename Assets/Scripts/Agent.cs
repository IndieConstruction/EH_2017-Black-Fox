﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Agent : MonoBehaviour, IShooter, IDamageable {

    public PlayerID playerID;
    float life = 10;                                                         // Vita

    List<GameObject> DamageablesPrefabs;                                            // Lista di Oggetti passati attraverso unity
    List<IDamageable> Damageables = new List<IDamageable>();                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable

    MovementController movment;
    PlacePin pinPlacer;
    Shoot shoot;
    GameManager gameManager;

    public float fireRate;                                                   // rateo di fuoco in secondi
    float nextFire;

    bool isAlive = true;                                                    // Indica se l'agente è vivo o morto.

    /// <summary>
    /// La vita dell'avatar
    /// </summary>
    public float Life
    {
        get { return life; }
        set { life = value;}
    }

    /// <summary>
    /// Stato dell'avatar
    /// </summary>
    public bool IsAlive
    {
        get { return isAlive; }
    }

    void Start ()
    {
        gameManager = GameManager.Instance;
        movment = GetComponent<MovementController>();
        pinPlacer = GetComponent<PlacePin>();
        shoot = GetComponent<Shoot>();
        LoadIDamageablePrefab();
    }

    void Update()
    {
        InputReader();
    }

    /// <summary>
    /// Salva all'interno della lista di oggetti IDamageable, gli oggetti facenti parti della lista DamageablesPrefabs
    /// </summary>
    private void LoadIDamageablePrefab()
    {

        //WARNING - se l'oggetto che che fa parte della lista di GameObject non ha l'interfaccia IDamageable non farà parte degli oggetti danneggiabili.

        DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", gameObject);      

        foreach (var k in DamageablesPrefabs)
        {
            if (k.GetComponent<IDamageable>() != null)
                Damageables.Add(k.GetComponent<IDamageable>());
        }
    }

    #region Controller Input

    /// <summary>
    /// Racchiude i controlli per piazzare i chiodi, sparare, ruotare e muoversi
    /// </summary>
    void InputReader()
    {
        if (Input.GetButtonDown(string.Concat("Joy" + ((int)playerID + 1) + "_RightBumper")))                       // place right pin
        {
            pinPlacer.ChangePinSpawnPosition("Right");
            pinPlacer.placeThePin();
        }
        
        if (Input.GetButtonDown(string.Concat("Joy" + ((int)playerID + 1) + "_LeftBumper")))                        // place left pin
        {
            pinPlacer.ChangePinSpawnPosition("Left");
            pinPlacer.placeThePin();
        }

        if (Input.GetButtonDown(string.Concat("Joy" + ((int)playerID + 1) + "_ButtonA")))                            // shoot
        {
            nextFire = Time.time + fireRate;
            shoot.ShootBullet();
            nextFire = Time.time + fireRate;
        }
        else if (Input.GetButton(string.Concat("Joy" + ((int)playerID + 1) + "_ButtonA")) && Time.time > nextFire)       // shoot at certain rate
        {
            nextFire = Time.time + fireRate;
            shoot.ShootBullet();
        }

        // Ruota e Muove l'agente
        /* 
         * Appunto sul funzionamento degli assi dei Trigger :
         * - se imposto ad entrambi i Trigger il 3th axis, uno funziona come l'opposto dell'altro, anche se ne viene usato solamente uno dallo script (es. Accelera e Frena).
         * - per separarne l'uso vanno settati al Left Trigger il 9th axis e al Right Trigger il 10th axis.
         * - modificarli a seconda dell'uso che se ne vuole fare.
         */

        /*
         *  Rotazione in base allo schermo -- NON CANCELLARE !
         *  Vector3 faceDirection = new Vector3(Input.GetAxis(string.Concat("Joy" + ((int)playerID) + "_LeftStickHorizontal")), 0f, Input.GetAxis(string.Concat("Joy" + ((int)playerID) + "_LeftStickVerical"))); 
         *  movment.RotationTowards(faceDirection); 
         */

        float thrust = Input.GetAxis(string.Concat("Joy" + ((int)playerID + 1) + "_RightTrigger"));              // Add thrust   
        movment.Rotation(Input.GetAxis(string.Concat("Joy" + ((int)playerID + 1) + "_LeftStickHorizontal")));  // Ruota l'agente
        movment.Movement(thrust);                                                   // Muove l'agente                                                                                
    }
    #endregion

    #region Interfaces

    #region IShooter
    /// <summary>
    /// Ritorna la lista degli oggetti danneggiabili
    /// </summary>
    /// <returns></returns>
    public List<IDamageable> GetDamageable()
    {
        return Damageables;
    }

    /// <summary>
    /// Ritorna il gameobject a cui è attaccato il component
    /// </summary>
    /// <returns></returns>
    public Agent GetOwner()
    {
        return this;
    }

    #endregion

    #region IDamageable
    /// <summary>
    /// Danneggia la vita dell'agente a cui è attaccato e ritorna i punti da assegnare all'agente che lo ha copito
    /// </summary>
    /// <param name="_damage">La quantità di danni che subisce</param>
    /// <returns></returns>
    public void Damage(float _damage)
    {
        if (isAlive)
        {
            Life -= _damage;
            if (Life < 1)
            {
                isAlive = false;
                gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #endregion
}

public enum PlayerID
{
    One, Two, Three, Four
}


