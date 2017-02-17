using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamUtility.IO;
using System;

public class Player : MonoBehaviour, IShooter, IDamageable {

    [SerializeField]
    PlayerID playerID;

    float life = 10;
    public float points = 0;
    float killPoints = 100;

    public List<GameObject> DamageablesPrefabs = new List<GameObject>();            // Lista di Oggetti passati attraverso unity
    List<IDamageable> Damageables = new List<IDamageable>();                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable.

    MovementController movment;
    PlacePin pinPlacer;
    Shoot shoot;

    bool isAlive = true;        // Indica se l'agente è vivo o morto.
    
    /// <summary>
    /// La vita dell'agente
    /// </summary>
    float Life
    {
        get { return life; }
        set { life = value; }
    }

    /// <summary>
    /// Punti dell'agente
    /// </summary>
    public float Points
    {
        get { return points; }
        set { points += value; }
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
    }

    

    /// <summary>
    /// Salva all'interno della lista di oggetti IDamageable, gli oggetti facenti parti della lista DamageablesPrefabs
    /// </summary>
    private void LoadIDamageablePrefab()
    {
        foreach (var k in DamageablesPrefabs)
        {
            if (k.GetComponent<IDamageable>() != null)                  //WARNING\\ se l'oggetto che che fa parte della lista di GameObject non ha l'interfaccia IDamageable
                Damageables.Add(k.GetComponent<IDamageable>());         // non farà parte degli oggetti danneggiabili.
        }
    }

    #region Controller Input
    /// <summary>
    /// Racchiude i controlli per piazzare i chiodi, sparare, ruotare e muoversi
    /// </summary>
    void ActionReader()
    {
        if (InputManager.GetButtonDown("Right Bumper", playerID))                       // place right pin
        {
            pinPlacer.ChangePinSpawnPosition("Right");
            pinPlacer.placeThePin();
        }

        if (InputManager.GetButtonDown("Left Bumper", playerID))                        // place left pin
        {
            pinPlacer.ChangePinSpawnPosition("Left");
            pinPlacer.placeThePin();
        }

        if (InputManager.GetButtonDown("Button A", playerID))                            // shoot
        {
            shoot.ShootBullet();
        }


        // Ruota e Muove l'agente
        float thrust = InputManager.GetAxis("Right Trigger", playerID);                                                                                             // Add thrust
        Vector3 faceDirection = new Vector3(InputManager.GetAxis("Left Stick Horizontal", playerID), 0f, InputManager.GetAxis("Left Stick Vertical", playerID));    // Indica in che direzione ruotare l'agente
        movment.Movement(-thrust);                                                                                                                                  // Il grilletto destro ritorna un valore negativo
        movment.RotationTowards(faceDirection);                                                                                                                     // Ruota
    }

    /// <summary>
    /// -Al momento non utilizzata-
    /// </summary>
    /// <param name="_axis"></param>
    /// <param name="_parameter"></param>
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
    public GameObject GetOwner()
    {
        return gameObject;
    }

    #endregion

    #region IDamageable
    /// <summary>
    /// Danneggia la vita dell'agente a cui è attaccato e ritorna i punti da assegnare all'agente che lo ha copito
    /// </summary>
    /// <param name="_damage">La quantità di danni che subisce</param>
    /// <returns></returns>
    public float Damage(float _damage)
    {
        if(isAlive)                                         //Controlla se l'agente è vivo
        {
            Life -= _damage;                                //Diminuisce la vita dell'agente in base ai danni passatigli da _damage
            if (Life < 1)                                   //Controlla se dopo aver danneggiato l'agente, la sua vita è arrivata a 0
            {                                               
                isAlive = false;                            // se è uguale a 0, isAlive diventa false
                Debug.Log("Sei Morto n# " + playerID);      
                gameObject.SetActive(false);                //Disattiva l'agente
                return killPoints;                          //Ritorna i punti da assegnare a chi ha ucciso
            }
            else                                            //Se l'agente rimane vivo ritorna 0.
            {
                Debug.Log("Damage : " + _damage + " # Life : " + Life);
                return 0;
            }
        }
        return 0;
    }
    #endregion

    #endregion
}
