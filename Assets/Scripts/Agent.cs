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
        float life = 10;                                // Vita                

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
        GameManager gameManager;
        RopeController rope;

        public float fireRate;                                                   // rateo di fuoco in secondi
        float nextFire;
        float ropeExtTimer;

        void Start()
        {
            gameManager = GameManager.Instance;
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
            //WARNING - se l'oggetto che che fa parte della lista di GameObject non ha l'interfaccia IDamageable non farà parte degli oggetti danneggiabili.

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
                pinPlacer.placeThePin(this);
            }

            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_PlaceLeft")))                        // place left pin
            {
                pinPlacer.placeThePin(this);
            }

            if (Input.GetButtonDown(string.Concat("Key" + (int)playerIndex + "_Fire")))       // shoot 
            {
                nextFire = Time.time + fireRate;
                shooter.ShootBullet();
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
        /// <summary>
        /// Controlla l'input da controller
        /// </summary>
        void XInputReader()
        {
            prevState = state;
            state = GamePad.GetState(playerIndex);

            movment.Movement(state.Triggers.Right);
            movment.Rotation(state.ThumbSticks.Left.X);

            ropeExtTimer += Time.deltaTime;
            if (state.Triggers.Right >= 0.9f && ropeExtTimer >= 0.1f)
            {
                rope.ExtendRope();
                ropeExtTimer = 0;
            }
                


            if (prevState.Buttons.RightShoulder == ButtonState.Released && state.Buttons.RightShoulder == ButtonState.Pressed)
            {
                pinPlacer.placeThePin(this);
            }

            if (prevState.Buttons.LeftShoulder == ButtonState.Released && state.Buttons.LeftShoulder == ButtonState.Pressed)
            {
                pinPlacer.placeThePin(this);
            }

            if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed)
            {
                nextFire = Time.time + fireRate;
                shooter.ShootBullet();
            }
            else if (prevState.Buttons.A == ButtonState.Pressed && state.Buttons.A == ButtonState.Pressed && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                shooter.ShootBullet();
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
            life -= _damage;

            if (life < 1)
            {
                if (AgentKilled != null)
                    AgentKilled(_attacker.GetComponent<Agent>(), this);
                else
                    AgentKilled(null, this);
                Destroy(gameObject);
            }
        }


        #endregion

        #endregion

        #region Events

        public delegate void AgentEvent(Agent _killer, Agent _victim);

        public static AgentEvent AgentKilled;

        #endregion
    }
}


