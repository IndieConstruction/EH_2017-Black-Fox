using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public abstract class PowerUpBase : MonoBehaviour, IPowerUp {
        public bool AutoUse = false;
        protected IPowerUpCollector collector;
        public abstract void UsePowerUp();

        protected virtual void NotifyCollect(IPowerUpCollector _collector) {
            _collector.CollectPowerUp(this);
        }

        private void OnTriggerEnter(Collider other) {
            collector = other.GetComponentInParent<IPowerUpCollector>();
            if (collector != null) {
                NotifyCollect(collector);
                if (AutoUse)
                    UsePowerUp();
            }
        }
    }
}
