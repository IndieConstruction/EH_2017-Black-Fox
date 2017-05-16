using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public abstract class PowerUpBase : MonoBehaviour, IPowerUp {
        public bool AutoUse = false;
        protected IPowerUpCollector collector;
        protected List<IPowerUpCollector> enemyCollectors = new List<IPowerUpCollector>();
        public abstract void UsePowerUp();

        protected virtual void NotifyCollect(IPowerUpCollector _collector) {
            _collector.CollectPowerUp(this);
        }

        private void OnTriggerEnter(Collider other) {
            collector = other.GetComponentInParent<IPowerUpCollector>();
            foreach (Player player in other.GetComponentInParent<Avatar>().Enemies)
            {
                enemyCollectors.Add(player.Avatar);
            }
            if (collector != null) {
                NotifyCollect(collector);
                if (AutoUse)
                    UsePowerUp();
            }
        }
    }
}
