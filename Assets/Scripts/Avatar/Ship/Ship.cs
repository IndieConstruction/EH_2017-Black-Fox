using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlackFox {
    [RequireComponent(typeof(MovementController), typeof(PlacePin), typeof(Shooter))]
    public class Ship : MonoBehaviour, IShooter, IDamageable {

        public GameObject[] ObjToBeColored;

        [HideInInspector]
        public Avatar avatar;

        ShipConfig config;

        MovementController movment;
        PlacePin pinPlacer;
        AvatarUI avatarUi;
        Tweener damageTween;

        //Life fields
        public float MaxLife = 10;
        private float _life = 10;
        public float Life {
            get { return _life; }
            private set {
                _life = value;
                if (avatarUi != null)
                    avatarUi.SetLifeSliderValue(this);
            }
        }

        private void Update() {
            if (avatar.State == AvatarState.Enabled) {
                CheckInputStatus(avatar.Player.InputStatus);
            }
        }

        #region API
        public void Setup(Avatar _avatar, List<IDamageable> _damageablesPrefabs) {
            avatar = _avatar;
            rigid = GetComponent<Rigidbody>();
            Shooter = GetComponent<Shooter>();
            movment = GetComponent<MovementController>();
            pinPlacer = GetComponent<PlacePin>();
            avatarUi = GetComponentInChildren<AvatarUI>();
            pinPlacer.SetOwner(this);
            damageables = _damageablesPrefabs;
        }

        /// <summary>
        /// Initialize initial values of Avatar
        /// </summary>
        public void Init() {
            Life = MaxLife;
        }
        #endregion

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
                GameManager.Instance.LevelMng.PauseGame(avatar.Player.ID);
            }
        }

        #region Shoot

        public Shooter Shooter { get; private set; }

        //Shooting fields
        public float fireRate;
        float nextFire;

        /// <summary>
        /// List of element damageable by this player
        /// </summary>
        List<IDamageable> damageables = new List<IDamageable>();

        /// <summary>
        /// Chiama la funzione AddAmmo di shooter
        /// </summary>
        public void AddShooterAmmo() {
            Shooter.AddAmmo();
            avatar.OnAmmoUpdate(Shooter.ammo);                          // Ci sarà sempre un avatar?
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
            if (Life < 1) {
                avatar.ShipDestroy(_attacker.GetComponent<Ship>().avatar);
                GetComponent<CapsuleCollider>().enabled = false;
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { Destroy(gameObject); });
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
        public void ToggleAbilities(bool _active = true) {

            pinPlacer.enabled = _active;
            Shooter.enabled = _active;
            movment.enabled = _active;
        }

        void Shoot() {
            Shooter.ShootBullet();
            avatar.OnAmmoUpdate(Shooter.ammo);
        }

        void PlacePin(bool _isRight) {
            pinPlacer.placeThePin(_isRight);
        }

        void GoForward(float _amount) {
            movment.Movement(_amount);
            if (avatar.rope != null)
                ExtendRope(_amount);
        }

        void Rotate(float _amount) {
            movment.Rotation(_amount);
        }

        void ExtendRope(float _amount) {
            if (_amount >= .95f) {
                avatar.rope.ExtendRope(1);
            }
            previousSpeed = rigid.velocity;
        }
        #endregion
        
    }

    [Serializable]
    public class ShipConfig
    {
        public Ship Prefab;
        public List<Material> Materials;
    }
}