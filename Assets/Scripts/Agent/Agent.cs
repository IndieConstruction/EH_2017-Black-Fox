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
        public bool UseKeyboard;
        GamePadState state;
        GamePadState prevState;
        public PlayerIndex playerIndex;
        KeyCode SwitchInput;

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

            if (playerIndex == PlayerIndex.Three)
            {
                SwitchInput = KeyCode.F3;
            }
            else if (playerIndex == PlayerIndex.Four)
            {
                SwitchInput = KeyCode.F4;
            }
        }

        void FixedUpdate()
        {
            ///Dato il problema che quando entra nello start, ancora non sono stati passati i riferimenti delle slider allo UIManager
            /// questo fa guadangnare il tempo necessario perchè lo UIManager possa riempire i riferimenti così che Agent li possa usare

            if (Input.GetKeyDown(SwitchInput))
            {
                UseKeyboard = !UseKeyboard;
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
        /// Chiama la funzione AddAmmo di shooter
        /// </summary>
        public void AddShooterAmmo()
        {
            shooter.AddAmmo();
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

        #region KeyboardInput
        /// <summary>
        /// Controlla l'input da tastiera
        /// </summary>
        void KeyboardReader()
        {
            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceRight")))                       // place right pin
            {
                PlacePin();
            }

            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceLeft")))                        // place left pin
            {
                PlacePin();
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
                PlacePin();
            }

            if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                PlacePin();
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
                if (OnAgentKilled != null)
                    if (_attacker.GetComponent<Agent>() != null)
                        OnAgentKilled(_attacker.GetComponent<Agent>(), this);
                    else
                        OnAgentKilled(null, this);
                Destroy(gameObject);
            }
        }


        #endregion

        #endregion

        #region Events

        public delegate void AgentKilledEvent(Agent _killer, Agent _victim);

        public static AgentKilledEvent OnAgentKilled;


        public delegate void AgentDataChangedEvent(Agent _agent);

        public AgentDataChangedEvent OnDataChange;

        #endregion

        #region Player Abilities

        void Shoot() {
            shooter.ShootBullet();
        }

        void PlacePin() {
            pinPlacer.placeThePin(this);
        }

        void GoForward(float _amount) {
            movment.Movement(_amount);
            ExtendRope(_amount);
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