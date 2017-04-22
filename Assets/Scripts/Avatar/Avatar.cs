using System.Collections.Generic;
using UnityEngine;
using Rope;

namespace BlackFox {
    public class Avatar : MonoBehaviour{
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

        public Material ColorMaterial { get { return Model.shipConfig.material; } }
        /// <summary>
        /// Reference of the model to visualize
        /// </summary>
        private AvatarData _model;
        public AvatarData Model {
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

        public RopeController rope;
        Ship ship;

        private void OnDestroy() {
            if (transform.parent != null)
                State = AvatarState.Disabled;
            if(rope != null)
                Destroy(rope.gameObject);
        }

        /// <summary>
        /// Menage the state switches
        /// </summary>
        /// <param name="_newState"></param>
        /// <param name="_oldState"></param>
        void OnStateChange(AvatarState _newState, AvatarState _oldState) {
            switch (_newState) {
                case AvatarState.Disabled:
                    ship.ToggleAbilities(false);
                break;
                case AvatarState.Ready:
                    ship.Init();
                break;
                case AvatarState.Enabled:
                    ship.ToggleAbilities(true);
                break;
                default:
                break;
            }
        }

        #region API
        /// <summary>
        /// Required to setup the player (also launched on Start of this class)
        /// </summary>
        public void Setup(Player _player) {
            Player = _player;
            ship.Setup(this);
            if (GameManager.Instance.LevelMng.RopeMng != null && rope == null)
                GameManager.Instance.LevelMng.RopeMng.AttachNewRope(this);
            State = AvatarState.Ready;
        }

        /// <summary>
        /// L'ancia un evento alla distruzione della ship, 
        /// passando come parametri chi ha distrutto la ship e l'ID del player a cui appartiene
        /// </summary>
        /// <param name="_attacker">L'avatar che ha distrutto la ship</param>
        public void ShipDestroy(Avatar _attacker) {
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
        public void OnAmmoUpdate(int _ammo)
        {
            EventManager.OnAmmoValueChanged(Player.ID, _ammo);
        }


        #endregion
    }

    public enum AvatarState {
        Disabled = 0,
        Ready = 1,
        Enabled = 2
    }
}