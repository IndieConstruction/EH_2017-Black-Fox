using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class InvertCommands : PowerUpBase
    {
        bool CanDecrement = false;
        float timer;

        private void Start()
        {
            DestroyAfterUse = false;
            timer = 10;
        }

        public override void DestroyGameObjectAfterTime()
        {
            if (CanDecrement)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                    Destroy(gameObject);

                if ((collector as Avatar).State == AvatarState.Disabled)
                {
                    foreach (IPowerUpCollector enemy in enemyCollectors)
                    {
                        (enemy as Avatar).InvertCommands(0);
                    }

                    Destroy(gameObject);
                }
            }
            else
                base.DestroyGameObjectAfterTime();

            

        }

        public override void UsePowerUp()
        {
            CanDecrement = true;
            TogleAbility(false);
            foreach (IPowerUpCollector enemy in enemyCollectors)
            {
                (enemy as Avatar).InvertCommands(PowerUpDuration);
            }
            
        }

        void TogleAbility(bool _value)
        {
            GetComponent<Collider>().enabled = _value;
            GetComponent<MeshRenderer>().enabled = _value;
        }
    }
}