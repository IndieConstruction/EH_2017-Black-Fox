using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Avatar : MonoBehaviour, IShooter, IDamageable {

    public PlayerID playerID;

    public float life = 10;                                                         // Vita
    public float powerPoints = 100;                                                 //Punti Potenziamneto
    public string PlayerName;                                                       //Il nome del Player da associare all'avatar



    UIDisplay displatLife;

    List<GameObject> DamageablesPrefabs;                                            // Lista di Oggetti passati attraverso unity
    List<IDamageable> Damageables = new List<IDamageable>();                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable

    MovementController movment;
    PlacePin pinPlacer;
    Shoot shoot;

    public float fireRate;                                                   // rateo di fuoco in secondi
    float nextFire;

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
    public float PowerPoints
    {
        get { return powerPoints; }
        set { powerPoints += value; }
    }

    void Start ()
    {
        movment = GetComponent<MovementController>();
        pinPlacer = GetComponent<PlacePin>();
        shoot = GetComponent<Shoot>();
        displatLife = GetComponent<UIDisplay>();

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
         *  Vector3 faceDirection = new Vector3(Input.GetAxis(string.Concat("Joy" + ((int)playerID + 1) + "_LeftStickHorizontal")), 0f, Input.GetAxis(string.Concat("Joy" + ((int)playerID + 1) + "_LeftStickVerical"))); 
         *  movment.RotationTowards(faceDirection); 
         */

        float thrust = Input.GetAxis(string.Concat("Joy" + ((int)playerID + 1) + "_RightTrigger"));              // Add thrust   
        movment.Rotation(Input.GetAxis(string.Concat("Joy" + ((int)playerID + 1) + "_LeftStickHorizontal")));  // Ruota l'agente
        movment.Movement(thrust);                                                   // Muove l'agente                                                                                
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
    public void Damage(float _damage)
    {
        if(isAlive)                                         //Controlla se l'agente è vivo
        {
            Life -= _damage;                                //Diminuisce la vita dell'agente in base ai danni passatigli da _damage
            displatLife.SetSliderValue(Life);

            if (Life < 1)                                   //Controlla se dopo aver danneggiato l'agente, la sua vita è arrivata a 0
            {                                               
                isAlive = false;                            // se è uguale a 0, isAlive diventa false    
                gameObject.SetActive(false);                //Disattiva l'agente
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
