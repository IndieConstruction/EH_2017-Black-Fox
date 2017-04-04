using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using Rope;
using DG.Tweening;

namespace BlackFox
{
    [RequireComponent (typeof(MovementController), typeof(PlacePin), typeof(Shooter))]
    public class Agent : MonoBehaviour, IShooter, IDamageable
    {
        public PlayerIndex playerIndex;

        public float maxLife = 10;
        private float _life = 10;

        public float Life
        {
            get { return _life; }
            private set
            {
                _life = value;
                if (OnDataChange != null)  
                    OnDataChange(this);
            }
        }

        List<IDamageable> damageables = new List<IDamageable>();                        

        MovementController movment;
        PlacePin pinPlacer;
        Shooter shooter;
        RopeController rope;
        GameUIController UIController;

        public float fireRate;                                                   
        float ropeExtTimer;

        //Variabili per gestire la fisca della corda
        Rigidbody rigid;
        Vector3 previousSpeed;

        void Start()
        {
            rigid = GetComponent<Rigidbody>();
            movment = GetComponent<MovementController>();
            rope = SearchRope();
            pinPlacer = GetComponent<PlacePin>();
            shooter = GetComponent<Shooter>();
            UIController = FindObjectOfType<GameUIController>();
            shooter.playerIndex = this.playerIndex;
            LoadIDamageablePrefab();
        }

        private void OnDestroy()
        {
            if(transform.parent != null)
                Destroy(transform.parent.gameObject);
            GameManager.Instance.LevelMng.RopeMng.DestroyRope(this);
        }

        /// <summary>
        /// Cerca il rope controller associato all'agente
        /// </summary>
        /// <returns></returns>
        RopeController SearchRope()
        {
            foreach (RopeController rope in FindObjectsOfType<RopeController>())
            {
                if (rope.name == playerIndex + "Rope")
                    return rope;
            }
            return null;
        }

        /// <summary>
        /// Salva all'interno della lista di oggetti IDamageable, gli oggetti facenti parti della lista DamageablesPrefabs
        /// </summary>
        void LoadIDamageablePrefab()
        { 
            List<GameObject> DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", gameObject);

            foreach (var k in DamageablesPrefabs)
            {
                if (k.GetComponent<IDamageable>() != null)
                    damageables.Add(k.GetComponent<IDamageable>());
            }
        }

        /// <summary>
        /// Aggiorna la quantità di proiettili disponibili nel CanvasGame
        /// </summary>
        void SetAmmoInTheUI()
        {
            if (UIController != null)
                UIController.SetBulletsValue(playerIndex, shooter.ammo);
        }

        #region API
        /// <summary>
        /// Chiama la funzione AddAmmo di shooter
        /// </summary>
        public void AddShooterAmmo()
        {
            shooter.AddAmmo();
            SetAmmoInTheUI();
        }

        /// <summary>
        /// Richiamata dal LevelManager per impostare i punti uccisione nella UI
        /// </summary>
        /// <param name="_playerIndex">L'agente a cui aggiornare la UI</param>
        /// <param name="_playerPoints">I punti che ha l'agente</param>
        public void UpdateKillPointsInUI(PlayerIndex _playerIndex, int _playerPoints)
        {
            // richiama la funzione in UIControlle per aggiornare i punti sulla propria UI
            UIController.SetKillPointsUI(_playerIndex, _playerPoints);
        }

        public Shooter GetShooterReference()
        {
            return shooter;
        }
        #endregion

        #region Player Abilities

        public void Shoot()
        {
            shooter.ShootBullet();
            SetAmmoInTheUI();
        }

        public void PlacePin(bool _isRight)
        {
            pinPlacer.placeThePin(this, _isRight);
        }

        public void GoForward(float _amount)
        {
            movment.Movement(_amount);
            if (rope != null)
                ExtendRope(_amount);
        }

        public void Rotate(float _amount)
        {
            movment.Rotation(_amount);
        }

        public void ExtendRope(float _amount)
        {
            ;
            if (_amount >= .95f)
            {
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
        Tweener damageTween;
        /// <summary>
        /// Danneggia la vita dell'agente a cui è attaccato e ritorna i punti da assegnare all'agente che lo ha copito
        /// </summary>
        /// <param name="_damage">La quantità di danni che subisce</param>
        /// <returns></returns>
        public void Damage(float _damage, GameObject _attacker)
        {
            if(damageTween != null)
                damageTween.Complete();

            Life -= _damage;            
            damageTween =  transform.DOPunchScale(new Vector3(0.2f, 0.2f, 0.2f), 0.5f);
            if (Life < 1)
            {
                if (EventManager.OnAgentKilled != null)
                {
                    if (_attacker != null)
                        EventManager.OnAgentKilled(_attacker.GetComponent<Agent>(), this);
                    else
                        EventManager.OnAgentKilled(null, this);
                }
                    
                transform.DOScale(Vector3.zero, 0.5f).OnComplete(() => { Destroy(gameObject); });              
                return;
            }
        }
        #endregion

        #endregion

        #region Events

        public delegate void AgentDataChangedEvent(Agent _agent);

        public AgentDataChangedEvent OnDataChange;

        #endregion

    }
}