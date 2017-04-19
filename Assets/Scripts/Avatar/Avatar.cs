using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using Rope;
using DG.Tweening;

namespace BlackFox {
    [RequireComponent(typeof(MovementController), typeof(PlacePin), typeof(Shooter))]
    public class Avatar : MonoBehaviour, IShooter, IDamageable {
        /// <summary>
        /// Player who control this avatar
        /// </summary>
        public Player Player;
        /// <summary>
        /// Index of th e player
        /// </summary>
        public PlayerLabel PlayerId {
            get {
                if (Player == null)
                    return PlayerLabel.None;
                return Player.ID;
            }
        }

        public Material ColorMaterial { get { return Model.material; } }
        /// <summary>
        /// Reference of the model to visualize
        /// </summary>
        private ShipModel _model;
        public ShipModel Model {
            get { return _model; }
            set { _model = value; }
        }

        private AvatarState _state;
        public AvatarState State {
            get { return _state; }
            set {
                if (Player != null) {
                    OnStateChange(value, _state);
                    _state = value;
                }
            }
        }

        //Life fields
        public float MaxLife = 10;
        private float _life = 10;
        public float Life {
            get { return _life; }
            private set {
                _life = value;
                if (OnDataChange != null)
                    OnDataChange(this);
            }
        }
        //Shooting fields
        public float fireRate;
        float nextFire;
        /// <summary>
        /// List of element damageable by this player
        /// </summary>
        List<IDamageable> damageables = new List<IDamageable>();

        MovementController movment;
        PlacePin pinPlacer;
        Shooter shooter;
        public RopeController rope;
        GameUIController UIController;
        //Variabili per gestire la fisca della corda
        Rigidbody rigid;
        Vector3 previousSpeed;

        void Start() {
            UIController = FindObjectOfType<GameUIController>();
            rigid = GetComponent<Rigidbody>();
            shooter = GetComponent<Shooter>();
            movment = GetComponent<MovementController>();
            pinPlacer = GetComponent<PlacePin>();

            LoadIDamageablePrefab();
        }

        private void Update() {
            if (State == AvatarState.Enabled) {
                CheckInputStatus(Player.inputStatus); 
            }
        }

        private void OnDestroy() {
            if (transform.parent != null)
                State = AvatarState.Disabled;
            
        }
        /// <summary>
        /// Menage the state switches
        /// </summary>
        /// <param name="_newState"></param>
        /// <param name="_oldState"></param>
        void OnStateChange(AvatarState _newState, AvatarState _oldState) {
            switch (_newState) {
                case AvatarState.Disabled:
                    ToggleAbilities(false);
                break;
                case AvatarState.Ready:
                    Init();
                break;
                case AvatarState.Enabled:
                    ToggleAbilities(true);
                break;
                default:
                break;
            }
        }
        /// <summary>
        /// Initialize initial values of Avatar
        /// </summary>
        private void Init() {
            Life = MaxLife;

            if (GameManager.Instance.LevelMng.RopeMng != null && rope == null)
                GameManager.Instance.LevelMng.RopeMng.AttachNewRope(this);
        }

        void CheckInputStatus(InputStatus _inputStatus) {
            GoForward(_inputStatus.RightTriggerAxis);
            Rotate(_inputStatus.LeftThumbSticksAxisX);

            if (_inputStatus.RightShoulder == ButtonState.Pressed) {
                PlacePin(true);
            }

            if (_inputStatus.LeftShoulder == ButtonState.Pressed) {
                PlacePin(false);
            }

            if (_inputStatus.A == ButtonState.Pressed) {
                nextFire = Time.time + fireRate;
                Shoot();
            } else if (_inputStatus.A == ButtonState.Held && Time.time > nextFire) {
                nextFire = Time.time + fireRate;
                Shoot();
            }

            if (_inputStatus.Start == ButtonState.Pressed) {
                GameManager.Instance.LevelMng.PauseGame(Player.ID);
            }
        }
        
        /// <summary>
        /// Salva all'interno della lista di oggetti IDamageable, gli oggetti facenti parti della lista DamageablesPrefabs
        /// </summary>
        void LoadIDamageablePrefab() {
            List<GameObject> DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", gameObject);

            foreach (var k in DamageablesPrefabs) {
                if (k.GetComponent<IDamageable>() != null)
                    damageables.Add(k.GetComponent<IDamageable>());
            }
        }

        /// <summary>
        /// Aggiorna la quantità di proiettili disponibili nel CanvasGame
        /// </summary>
        void SetAmmoInTheUI() {
            if (UIController != null)
                UIController.SetBulletsValue(Player.ID, shooter.ammo);
        }

        #region API
        /// <summary>
        /// Required to setup the player (also launched on Start of this class)
        /// </summary>
        public void Setup(Player _player) {
            Player = _player;
            pinPlacer.SetOwner(this);
            State = AvatarState.Ready;
        }

        /// <summary>
        /// Chiama la funzione AddAmmo di shooter
        /// </summary>
        public void AddShooterAmmo() {
            shooter.AddAmmo();
            SetAmmoInTheUI();
        }

        /// <summary>
        /// Richiamata dal LevelManager per impostare i punti uccisione nella UI
        /// </summary>
        /// <param name="_playerID">L'agente a cui aggiornare la UI</param>
        /// <param name="_playerPoints">I punti che ha l'agente</param>
        public void UpdateKillPointsInUI(PlayerLabel _playerID, int _playerPoints) {
            // richiama la funzione in UIControlle per aggiornare i punti sulla propria UI
            UIController.SetKillPointsUI(_playerID, _playerPoints);
        }

        public Shooter GetShooterReference() {
            return shooter;
        }
        #endregion

        #region Player Abilities
        /// <summary>
        /// Set all the Player abilities as active/inactive
        /// </summary>
        /// <param name="_active"></param>
        void ToggleAbilities(bool _active = true) {
            
            pinPlacer.enabled = _active;
            shooter.enabled = _active;
            movment.enabled = _active;
        }

        void Shoot() {
            shooter.ShootBullet();
            SetAmmoInTheUI();
        }

        void PlacePin(bool _isRight) {
            pinPlacer.placeThePin(_isRight);
        }

        void GoForward(float _amount) {
            movment.Movement(_amount);
            if (rope != null)
                ExtendRope(_amount);
        }

        void Rotate(float _amount) {
            movment.Rotation(_amount);
        }

        void ExtendRope(float _amount) {
            if (_amount >= .95f) {
                rope.ExtendRope(1);
            }
            previousSpeed = rigid.velocity;
        }
        #endregion

        #region Interfaces
        #region IShooter
        /// <summary>
        /// Ritorna la lista degli oggetti danneggiabili
        /// </summary>
        /// <returns></returns>
        public List<IDamageable> GetDamageable() {
            return damageables;
        }

        /// <summary>
        /// Ritorna il gameobject a cui è attaccato il component
        /// </summary>
        /// <returns></returns>
        public GameObject GetOwner() {
            return gameObject;
        }

        #endregion

        #region IDamageable
        Tweener damageTween;
        /// <summary>
        /// Danneggia la vita dell'agente a cui è attaccato e ritorna i punti da assegnare all'agente che lo ha copito
        /// </summary>
        /// <param name="_damage">La quantità di danni che subisce</param>
        /// <returns></returns>
        public void Damage(float _damage, GameObject _attacker) {
            if (damageTween != null)
                damageTween.Complete();

            Life -= _damage;
            damageTween = transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
            if (Life < 1) {
                if (EventManager.OnAgentKilled != null) {
                    if (_attacker != null)
                        EventManager.OnAgentKilled(_attacker.GetComponent<Avatar>(), this);
                    else
                        EventManager.OnAgentKilled(null, this);
                }
                GetComponent<CapsuleCollider>().enabled = false;
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { Destroy(gameObject); });
                return;
            }
        }
        #endregion
        #endregion

        #region Events
        public delegate void AgentDataChangedEvent(Avatar _agent);

        public AgentDataChangedEvent OnDataChange;
        #endregion
    }

    public enum AvatarState {
        Disabled = 0,
        Ready = 1,
        Enabled = 2
    }
}