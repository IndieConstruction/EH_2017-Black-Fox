using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XInputDotNetPure;

public class Agent : MonoBehaviour, IShooter, IDamageable {

    public bool UseKeyboard;
    public bool UseController;

    bool IsControllerUsed;

    // XInput Variables
    GamePadState state;
    GamePadState prevState;
    public PlayerIndex playerIndex;
    //####

    KeyCode SwitchInput;

    string Name;
    float life = 10;                                                         // Vita
    bool isAlive = true;                                                    // Indica se l'agente è vivo o morto.

    List<IDamageable> damageables = new List<IDamageable>();                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable

    MovementController movment;
    PlacePin pinPlacer;
    Shooter shooter;
    GameManager gameManager;

    //UIManager uiDisplay;

    public float fireRate;                                                   // rateo di fuoco in secondi
    float nextFire;

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
        //uiDisplay = GetComponent<UIDisplay>();

        gameManager = GameManager.Instance;
        movment = GetComponent<MovementController>();
        pinPlacer = GetComponent<PlacePin>();
        shooter = GetComponent<Shooter>();
        LoadIDamageablePrefab();
       gameManager.SetAgentSpawnPoint(playerIndex, transform);

        if (playerIndex == PlayerIndex.Three)
        {
            SwitchInput = KeyCode.F3;
        } else if (playerIndex == PlayerIndex.Four)
        {
            SwitchInput = KeyCode.F4;
        }
    }
    public void AddAmmo()
    {
        if(shooter.ammo <50)
        shooter.ammo += 10;
        //ammo=ammo+10;
        if (shooter.ammo > 50)
            shooter.ammo = 50;
    }



    void Update()
    {
        if (Input.GetKeyDown(SwitchInput))
        {
            if (UseKeyboard == true)
                UseKeyboard = false;
            else
                UseKeyboard = true;
        } 
        
        if (UseKeyboard)
        {
            KeyboardReader();
        }
        else
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

        List<GameObject> DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", gameObject);

        foreach (var k in DamageablesPrefabs)
        {
            if (k.GetComponent<IDamageable>() != null)
                damageables.Add(k.GetComponent<IDamageable>());
        }
    }

    #region KeyboardInput
    void KeyboardReader()
    {
        if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceRight")))                       // place right pin
        {
            pinPlacer.ChangePinSpawnPosition("Right");
            pinPlacer.placeThePin(this);
        }

        if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceLeft")))                        // place left pin
        {
            pinPlacer.ChangePinSpawnPosition("Left");
            pinPlacer.placeThePin(this);
        }

        
        if (Input.GetButton(string.Concat("Key" + (int)playerIndex + "_Fire")) && Time.time > nextFire)       // shoot at certain rate
        {
            nextFire = Time.time + fireRate;
            shooter.ShootBullet();
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
            pinPlacer.placeThePin(this);
        }

        if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
        {
            pinPlacer.ChangePinSpawnPosition("Left");
            pinPlacer.placeThePin(this);
        }

        if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
        {
            nextFire = Time.time + fireRate;
            shooter.ShootBullet();
            nextFire = Time.time + fireRate;
        }
        else if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Pressed && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            shooter.ShootBullet();
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
        return damageables;
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
    public void Damage(float _damage, GameObject _attacker)
    {
         Life -= _damage;
        //uiDisplay.SetSliderValue(Life);
        gameManager.uiManager.SetSliderValue(playerIndex, Life);
         
         if (Life < 1)
         {
            if (_attacker.GetComponent<Agent>() != null)
            {
                gameManager.SetKillPoints(_attacker.GetComponent<Agent>().playerIndex, playerIndex); 
            }
            Destroy(gameObject);
        }
    }     


    #endregion

    #endregion
}


