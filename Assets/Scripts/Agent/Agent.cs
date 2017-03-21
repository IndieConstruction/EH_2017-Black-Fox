using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using Rope;

namespace BlackFox
{
    [RequireComponent (typeof(MovementController), typeof(PlacePin), typeof(Shooter))]
    public class Agent : MonoBehaviour, IShooter, IDamageable
    {

        string Name;
        public float maxLife = 10;
        private float _life = 10;

        public float Life {
            get { return _life; }
            private set { _life = value;
                if (OnDataChange != null)  
                    OnDataChange(this);
            }
        }

        // Variabili per il funzionamento dei controller e della tastiera
        GamePadState state;
        GamePadState prevState;
        public PlayerIndex playerIndex;

        List<IDamageable> damageables = new List<IDamageable>();                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable

        MovementController movment;
        PlacePin pinPlacer;
        Shooter shooter;
        RopeController rope;

        public float fireRate;                                                   // rateo di fuoco in secondi
        float nextFire;
        float ropeExtTimer;

        void Start()
        {
            movment = GetComponent<MovementController>();
            rope = SearchRope();
            pinPlacer = GetComponent<PlacePin>();
            shooter = GetComponent<Shooter>();
            shooter.playerIndex = this.playerIndex;
            LoadIDamageablePrefab();
        }

        void FixedUpdate()
        {
            KeyboardReader();
            XInputReader();
        }

        private void OnEnable()
        {
            EventManager.OnLevelInit += HandleOnLevelInit;
            EventManager.OnLevelPlay += HandleOnLevelPlay;
            EventManager.OnLevelEnd += HandleOnLevelEnd;
        }

        private void OnDisable()
        {
            EventManager.OnLevelInit -= HandleOnLevelInit;
            EventManager.OnLevelPlay -= HandleOnLevelPlay;
            EventManager.OnLevelEnd -= HandleOnLevelEnd;
        }

        #region Event Handler
        void HandleOnLevelInit()
        { 
            Life = maxLife;
            shooter.ammo = 0;
            EnableComponents(true);   
        }
        void HandleOnLevelPlay() { }

        void HandleOnLevelEnd()
        {
            EnableComponents(true);
        }
        #endregion

        void EnableComponents(bool _value)
        {
            GetComponentInChildren<MeshRenderer>().enabled = _value;
            GetComponent<Collider>().enabled = _value;
            GetComponent<Shooter>().enabled = _value;
            GetComponent<PlacePin>().enabled = _value;
            GetComponent<MovementController>().enabled = _value;
        }

        RopeController SearchRope()
        {
            foreach (RopeController rope in FindObjectsOfType<RopeController>())
            {
                if (rope.name == "Rope" + playerIndex)
                    return rope;
            }
            return null;
        }

        /// <summary>
        /// Salva all'interno della lista di oggetti IDamageable, gli oggetti facenti parti della lista DamageablesPrefabs
        /// </summary>
        private void LoadIDamageablePrefab()
        { 
            List<GameObject> DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", gameObject);

            foreach (var k in DamageablesPrefabs)
            {
                if (k.GetComponent<IDamageable>() != null)
                    damageables.Add(k.GetComponent<IDamageable>());
            }
        }

        #region API
        /// <summary>
        /// Chiama la funzione AddAmmo di shooter
        /// </summary>
        public void AddShooterAmmo()
        {
            shooter.AddAmmo();
        }

        public Shooter GetShooterReference()
        {
            return shooter;
        }
        #endregion
        
        #region KeyboardInput
        /// <summary>
        /// Controlla l'input da tastiera
        /// </summary>
        void KeyboardReader()
        {
            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceRight")))                       // place right pin
            {
                PlacePin(true);
            }

            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceLeft")))                        // place left pin
            {
                PlacePin(false);
                
            }

            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_Fire")))       // shoot 
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }

            if (Input.GetButton(string.Concat("Key" + (int)playerIndex + "_Fire")) && Time.time > nextFire)       // shoot at certain rate
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }

            Rotate(Input.GetAxis(string.Concat("Key" + (int)playerIndex + "_Horizonatal")));  // Ruota l'agente
            GoForward(Input.GetAxis(string.Concat("Key" + (int)playerIndex + "_Forward")));                                                                               // Muove l'agente                                                                                
        }
        #endregion

        #region XInput
        /// <summary>
        /// Controlla l'input da controller
        /// </summary>
        void XInputReader()
        {
            prevState = state;
            state = GamePad.GetState(playerIndex);

            GoForward(state.Triggers.Right);
            Rotate(state.ThumbSticks.Left.X);



            if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                PlacePin(true);
            }

            if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                PlacePin(false);
            }

            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
            else if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Pressed && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Shoot();
            }
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

            if (Life < 1)
            {
                if (EventManager.OnAgentKilled != null)
                {
                    if (_attacker.GetComponent<Agent>() != null)
                        EventManager.OnAgentKilled(_attacker.GetComponent<Agent>(), this);
                }
                else
                {
                    EventManager.OnAgentKilled(null, this);
                }
                EnableComponents(false);

            }
        }


        #endregion

        #endregion

        #region Events

        public delegate void AgentDataChangedEvent(Agent _agent);

        public AgentDataChangedEvent OnDataChange;

        #endregion

        #region Player Abilities

        void Shoot() {
            shooter.ShootBullet();
        }

        void PlacePin(bool _isRight) {
            pinPlacer.placeThePin(this, _isRight);
        }

        void GoForward(float _amount) {
            movment.Movement(_amount);
            //ExtendRope(_amount);
        }

        void Rotate(float _amount) {
            movment.Rotation(_amount);
        }

        void ExtendRope(float _amount) {
            ropeExtTimer += Time.deltaTime;
            if (_amount >= 0.9f && ropeExtTimer >= 0.1f) {
                rope.ExtendRope();
                ropeExtTimer = 0;
            }
        }

        #endregion
    }
}