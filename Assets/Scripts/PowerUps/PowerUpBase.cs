using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public abstract class PowerUpBase : MonoBehaviour, IPowerUp {

        public PowerUpID ID;

        public bool AutoUse = false;
        protected IPowerUpCollector collector;
        protected List<IPowerUpCollector> enemyCollectors = new List<IPowerUpCollector>();
        public abstract void UsePowerUp();
        private float _lifeTime = 10;
        public float PowerUpDuration = 10;
        public float SpawnRatio;
        
        /// <summary>
        /// Variabile che determina se il powerup deve essere distrutto una volta raccolto o meno.
        /// </summary>
        protected bool DestroyAfterUse = true;

        public float LifeTime
        {
            get { return _lifeTime; }
            set { _lifeTime = value; }
        }

        protected virtual void NotifyCollect(IPowerUpCollector _collector) {
            _collector.CollectPowerUp(this);
        }

        private void Update()
        {
            LifeTime -= Time.deltaTime;
            if (LifeTime <= 0)
                Destroy(gameObject); 
        }

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponentInParent<IPowerUpCollector>() == null)
                return;

            collector = other.GetComponentInParent<IPowerUpCollector>();
            PowerUpDuration = (collector as Avatar).GetUpgrade(UpgardeTypes.PowerUpDurationUpgrade).CalculateValue(PowerUpDuration);
            foreach (Player player in other.GetComponentInParent<Avatar>().Enemies)
            {
                enemyCollectors.Add(player.Avatar);
            }
            if (collector != null) {
                NotifyCollect(collector);
                if (AutoUse)
                    UsePowerUp();
                if (DestroyAfterUse)
                    Destroy(gameObject); 
            }
        }
    }

    public enum PowerUpID
    {
        Kamikaze,
        AmmoCleaner,
        CleanSweep,
        Tank,
        InvertCommands
    }
}
