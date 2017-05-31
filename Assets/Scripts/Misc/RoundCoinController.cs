using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class RoundCoinController : MonoBehaviour
    {
        public GameObject CoinToInstantiate;

        CoinManager coinMng;

        float coinLife;

        private int _coinCollected;

        public int CoinCollected
        {
            get { return _coinCollected; }
            set
            {
                _coinCollected = value;
                //Aggiorna la UI
            }
        }

        public void Init(CoinManager _coinMng, float _coinLife)
        {
            CoinCollected = 0;
            coinLife = _coinLife;
            coinMng = _coinMng;
        }

        public void InstantiateCoin(Vector3 _spawnPosition)
        {
            Coin tempCoin = Instantiate(CoinToInstantiate, _spawnPosition, Quaternion.identity).GetComponent<Coin>();
            tempCoin.Init(this, coinLife);
        }

        /// <summary>
        /// Funzione da chiamare alla fine del round solo in caso di vittoria per salvare le monete accumulate durante il round appena conclutosi
        /// </summary>
        public void OnEndRoun()
        {
            coinMng.RoundCoin = CoinCollected;
        }
    }
}