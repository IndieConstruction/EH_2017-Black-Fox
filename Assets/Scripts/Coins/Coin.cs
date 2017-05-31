using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class Coin : MonoBehaviour {

        float lifeTime;

        RoundCoinController coinController;

        public void Init(RoundCoinController _controller, float _coinLife)
        {
            coinController = _controller;
            lifeTime = _coinLife;
        }


        private void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
                Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Ship>() != null)
            {
                coinController.CoinCollected++;
                Destroy(gameObject);
            }
        }
    }
}