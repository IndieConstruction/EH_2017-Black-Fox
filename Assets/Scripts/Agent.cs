using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XInputDotNetPure;

public class Agent : MonoBehaviour, IShooter, IDamageable, IKillable {

    public bool UseKeyboard;
    public bool UseController;
    public bool UseAutoSwitch;

    bool IsControllerUsed;

    // XInput Variables
    GamePadState state;
    GamePadState prevState;
    public PlayerIndex playerIndex;
    //####

    float life = 10;                                                         // Vita
    bool Killable = false;
    PlayerIndex PlayerWhoKillMe;


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
        if (UseAutoSwitch)
        {
            if (Input.anyKeyDown)
            {
                UseKeyboard = true;
            }
            else if(IsControllerUsed)
            {
                UseController = true;
            }
        }

        if (UseKeyboard)
        {
            KeyboardReader();
        }
        else if(UseController)
        {
            XInputReader();
        }
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

    #region KeyboardInput
    void KeyboardReader()
    {
        if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceRight")))                       // place right pin
        {
            pinPlacer.ChangePinSpawnPosition("Right");
            pinPlacer.placeThePin();
        }

        if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceLeft")))                        // place left pin
        {
            pinPlacer.ChangePinSpawnPosition("Left");
            pinPlacer.placeThePin();
        }

        if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_Fire")))                            // shoot
        {
            nextFire = Time.time + fireRate;
            shoot.ShootBullet();
            nextFire = Time.time + fireRate;
        }
        else if (Input.GetButton(string.Concat("Key" + (int)playerIndex + "_Fire")) && Time.time > nextFire)       // shoot at certain rate
        {
            nextFire = Time.time + fireRate;
            shoot.ShootBullet();
        }

        float thrust = Input.GetAxis(string.Concat("Key" + (int)playerIndex + "_Forward"));              // Add thrust   
        movment.Rotation(Input.GetAxis(string.Concat("Key" + (int)playerIndex + "_Horizonatal")));  // Ruota l'agente
        movment.Movement(thrust);                                                                               // Muove l'agente                                                                                
    }
    #endregion

    #region XInput

    void XInputReader()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);

        if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
        {
            pinPlacer.ChangePinSpawnPosition("Right");
            pinPlacer.placeThePin();
        }

        if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            pinPlacer.ChangePinSpawnPosition("Left");
            pinPlacer.placeThePin();
        }

        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            nextFire = Time.time + fireRate;
            shoot.ShootBullet();
            nextFire = Time.time + fireRate;
        }
        else if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Pressed && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shoot.ShootBullet();
            nextFire = Time.time + fireRate;
        }

        movment.Rotation(state.ThumbSticks.Left.X);
        movment.Movement(state.Triggers.Right);
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
            if (Life == 1)
            {
                Killable = true;
            }
            if (Life < 1)
            {
                isAlive = false;
                gameObject.SetActive(false);
            }
        }
    }


    #endregion

    #region ICollectablePoints

    public void CheckIfKillable(PlayerIndex _playerKiller)
    {
        if(Killable)
            gameManager.SetKillPoints(_playerKiller, playerIndex);
    }

    #endregion

    #endregion
}


