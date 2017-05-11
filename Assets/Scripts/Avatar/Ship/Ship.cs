using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlackFox {
    [RequireComponent(typeof(MovementController))]
    public class Ship : MonoBehaviour, IShooter, IDamageable
    {
        [HideInInspector]
        public Avatar Avatar;
        public ShipConfig Config
        {
            get { return Avatar.AvatarData.shipConfig; }
        }

        MovementController movment;
        PlacePin pinPlacer;
        AvatarUI avatarUi;
        Tweener damageTween;

        // Life fields
        private float _life;
        public float Life {
            get { return _life; }
            private set {
                _life = value;
                if(EventManager.OnLifeValueChange != null)
                    EventManager.OnLifeValueChange(Avatar);
            }
        }

        private void Update()
        {
            if (Avatar.State == AvatarState.Enabled)
            {
                CheckInputStatus(Avatar.Player.InputStatus);
            }
        }

        #region API
        public void Setup(Avatar _avatar, List<IDamageable> _damageablesPrefabs)
        {
            Avatar = _avatar;
            rigid = GetComponent<Rigidbody>();
            damageables = _damageablesPrefabs;
            ChangeColor(Config.ColorSets[Avatar.ColorSetIndex].ShipMaterialMain);

            shooter = GetComponentInChildren<Shooter>();
            shooter.Init(this);
            movment = GetComponent<MovementController>();
            movment.Init(this, rigid);
            pinPlacer = GetComponentInChildren<PlacePin>();
            pinPlacer.Setup(this);
            avatarUi = GetComponentInChildren<AvatarUI>();
        }

        /// <summary>
        /// Initialize initial values of Avatar
        /// </summary>
        public void Init()
        {
            Life = Config.MaxLife;
        }

        public void ChangeColor(Material _mat)
        {
            foreach (var m in GetComponentsInChildren<MeshRenderer>())
            {
                Material[] mats = new Material[] { _mat };
                m.materials = mats;
            } 
        }

        /// <summary>
        /// Remove all the placed Pins of this ship
        /// </summary>
        public void RemoveAllPins()
        {
            pinPlacer.RemoveAllPins();
        }
        #endregion

        // Input Fields
        Vector3 leftStickDirection;
        Vector3 rightStickDirection;

        void CheckInputStatus(InputStatus _inputStatus)
        {            
            leftStickDirection = new Vector3(_inputStatus.LeftThumbSticksAxisX, 0, _inputStatus.LeftThumbSticksAxisY);
            rightStickDirection = new Vector3(_inputStatus.RightThumbSticksAxisX, 0, _inputStatus.RightThumbSticksAxisY);

            Move(leftStickDirection);
            DirectFire(rightStickDirection);

            if (_inputStatus.RightShoulder == ButtonState.Pressed)
            {
                PlacePin();
            }

            if (_inputStatus.RightTrigger == ButtonState.Pressed)
            {
                Shoot();
                nextFire = Time.time + Config.FireRate;
            }
            else if (_inputStatus.RightTrigger == ButtonState.Held )
            {
                if(Time.time > nextFire) { 
                    Shoot();
                    nextFire = Time.time + FireRate;
                }
            }

            if (_inputStatus.Start == ButtonState.Pressed)
            {
                GameManager.Instance.LevelMng.PauseGame(Avatar.Player.ID);
            }
        }

        #region Shoot
        public Shooter shooter { get; private set; }

        public float FireRate { get { 
                    if (Avatar.GetUpgrade(UpgardeTypes.FireRateUpgrade) != null)
                        return Avatar.GetUpgrade(UpgardeTypes.FireRateUpgrade).CalculateValue(Config.FireRate);
                    else
                        return Config.FireRate;
                    }
        }

        //Shooting fields
        float nextFire;

        /// <summary>
        /// List of element damageable by this player
        /// </summary>
        List<IDamageable> damageables = new List<IDamageable>();

        /// <summary>
        /// Chiama la funzione AddAmmo di shooter
        /// </summary>
        public void AddShooterAmmo() {
            shooter.AddAmmo();
            //avatar.OnAmmoUpdate(shooter.Ammo);                          // Ci sarà sempre un avatar?
        }

        #region IShooter
        /// <summary>
        /// Ritorna la lista degli oggetti danneggiabili
        /// </summary>
        /// <returns></returns>
        public List<IDamageable> GetDamageable() {
            return damageables;
        }
        /// <summary>
        /// Return the one who shot
        /// </summary>
        /// <returns></returns>
        public GameObject GetOwner() {
            return gameObject;
        }
        #endregion

        #endregion

        #region IDamageable
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
            if (Life < 1)
            {
                Avatar.ShipDestroy(_attacker.GetComponent<Ship>().Avatar);
                transform.DOScale(Vector3.zero, 0.5f);
                return;
            }
        }
        #endregion

        #region Ship Abilities
        //Variabili per gestire la fisca della corda
        Rigidbody rigid;
        Vector3 previousSpeed;

        /// <summary>
        /// Set all the Player abilities as active/inactive
        /// </summary>
        /// <param name="_active"></param>
        public void ToggleAbilities(bool _active = true)
        {
            pinPlacer.enabled = _active;
            shooter.enabled = _active;
            movment.enabled = _active;
            GetComponent<CapsuleCollider>().enabled = _active;
        }

        void DirectFire(Vector3 _direction)
        {
            shooter.SetFireDirection(_direction);
        }

        void Shoot()
        {
            shooter.ShootBullet();
        }

        void PlacePin()
        {
            pinPlacer.PlaceThePin();
            AddShooterAmmo();
        }

        void Move(Vector3 _target)
        {
            movment.Move(_target);
            if (Avatar.rope != null)
                ExtendRope(_target.magnitude);
        }

        void ExtendRope(float _amount)
        {
            if (_amount >= .95f) {
                Avatar.rope.ExtendRope(1);
            }
            previousSpeed = rigid.velocity;
        }
        #endregion
    }

    [Serializable]
    public class ShipConfig
    {
        public Ship Prefab;
        public List<ColorSetData> ColorSets;
        [Header("Ship Parameters")]
        public float FireRate;
        public float MaxLife;
        public MovementControllerConfig movementConfig;
        public ShooterConfig shooterConfig;
        public PlacePinConfig placePinConfig;
    }
}