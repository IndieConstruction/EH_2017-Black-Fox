using System.Collections.Generic;
using UnityEngine;
using Rope;
using System;

namespace BlackFox {
    public class Avatar : MonoBehaviour, IPowerUpCollector {
        /// <summary>
        /// Player who control this avatar
        /// </summary>
        [HideInInspector]
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

        public int ColorSetIndex;

        /// <summary>
        /// Reference of the model to visualize
        /// </summary>
        public AvatarData AvatarData {
            get { return Player.AvatarData; }
        }

        private AvatarState _state;
        public AvatarState State {
            get { return _state; }
            set {
                if (Player != null) {
                    if (_state != value)
                        OnStateChange(value, _state);
                    _state = value;
                }
            }
        }


        #region Upgrade

        public List<IUpgrade> Upgrades = new List<IUpgrade>() {
            new FireRateUpgrade(new float[] {0f, 0.01f, 0.02f, 0.03f, 0.04f, 0.27f}), // TODO : valori da mettere nello scriptable dell'avatar
        };

        public IUpgrade GetUpgrade(UpgardeTypes _id) {
            foreach (IUpgrade upgrade in Upgrades) {
                if (upgrade.ID == _id)
                    return upgrade;
            }
            return null;
        }

        #endregion


        [HideInInspector]
        public RopeController rope;
        [HideInInspector]
        public Ship ship;
        AvatarUI avatarUI;

        /// <summary>
        /// Crea e prende riferimento dell'AvatarUI
        /// </summary>
        void CreateShipUI()
        {
            avatarUI = GameManager.Instance.UiMng.CreateAvatarUI(ship.gameObject);
        }

        /// <summary>
        /// Menage the state switches
        /// </summary>
        /// <param name="_newState"></param>
        /// <param name="_oldState"></param>
        void OnStateChange(AvatarState _newState, AvatarState _oldState)
        {
            switch (_newState)
            {
                case AvatarState.Disabled:
                    ship.RemoveAllPins();
                    ship.ToggleAbilities(false);
                    if (rope != null)
                    {
                        rope.DestroyDynamically();
                        rope = null;
                    }
                    break;
                case AvatarState.Ready:
                    Init();
                    rope.GetComponent<LineRenderer>().enabled = false;
                    break;
                case AvatarState.Enabled:
                    ship.ToggleAbilities(true);
                    ship.transform.localScale = Vector3.one;
                    rope.GetComponent<LineRenderer>().enabled = true;
                    break;
            }
        }

        #region API
        /// <summary>
        /// Required to setup the player (also launched on Start of this class)
        /// </summary>
        public void Setup(Player _player, bool withRope = true)
        {
            Player = _player;
            if (!ship)
                InstantiateShip();
            ColorSetIndex = (int)Player.ID;
            ship.Setup(this, LoadIDamageableForShip());
            CreateShipUI();
        }

        void Init(bool withRope = true)
        {
            ship.Init();
            if (withRope)
                SetupRope();
        }

        public void SetupRope()
        {
            if (GameManager.Instance.LevelMng.RopeMng != null && rope == null)
                GameManager.Instance.LevelMng.RopeMng.AttachNewRope(this);
        }

        public void InstantiateShip()
        {
            // TODO : controllare che la ship non sia doppia
            Transform transf = GameManager.Instance.LevelMng.AvatarSpwn.GetMySpawnPoint(PlayerId);
            ship = Instantiate(AvatarData.shipConfig.Prefab, transf.position, transf.rotation , transform).GetComponent<Ship>();
        }

        /// <summary>
        /// L'ancia un evento alla distruzione della ship, 
        /// passando come parametri chi ha distrutto la ship e l'ID del player a cui appartiene
        /// </summary>
        /// <param name="_attacker">L'avatar che ha distrutto la ship</param>
        public void ShipDestroy(Avatar _attacker)
        {
            State = AvatarState.Disabled;
            if (EventManager.OnAgentKilled != null) {
                if (_attacker != null)
                    EventManager.OnAgentKilled(_attacker, this);
                else
                    EventManager.OnAgentKilled(null, this);
            }
        }

        /// <summary>
        /// Scatena l'evento per aggiornare i proiettili nella UI
        /// </summary>
        /// <param name="_ammo">Le munizioni che rimangono</param>
        public void OnAmmoUpdate()
        {
            // TODO : da rivedere
            EventManager.OnAmmoValueChange(this);
        }

        #endregion

        /// <summary>
        /// Carica la lista dei damageable per la propria ship da resources
        /// </summary>
        List<IDamageable> LoadIDamageableForShip()
        {
            List<IDamageable> damageablesList = new List<IDamageable>();
            List<GameObject> damageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", ship.gameObject);

            foreach (GameObject itemInRes in damageablesPrefabs)
            {
                if (itemInRes.GetComponent<IDamageable>() != null)
                    damageablesList.Add(itemInRes.GetComponent<IDamageable>());
            }

            return damageablesList;
        }

        public void CollectPowerUp(IPowerUp _powerUp) {
            
        }
    }

    public enum AvatarState
    {
        Disabled = 0,
        Ready = 1,
        Enabled = 2
    }
}