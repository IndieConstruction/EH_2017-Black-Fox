using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class CoinManager : MonoBehaviour
    {

        public GameObject CoinControllerPrefab;

        [HideInInspector]
        public RoundCoinController CoinController;

        public float CoinLife = 10;


        public int RoundCoins = 0;

        private int _totalCoin;

        public int TotalCoin
        {
            get { return _totalCoin; }
            set { _totalCoin = value; }
        }

        private int _roundCoin;

        public int RoundCoin
        {
            get { return _roundCoin; }
            set { _roundCoin = value; }
        }
        
        public void InstantiateCoinController()
        {
            CoinController = Instantiate(CoinControllerPrefab, transform).GetComponent<RoundCoinController>();
            CoinController.Init(this, CoinLife);
        }
        
        /// <summary>
        ///dove segue ho messo degli appunti per ricordare per quando si farà il boss e lo spawn in altri casi delle monete 
        /// </summary>


        //void Spawncoin(GameObject coinprefab, Transform.position){ }

        //Instantiate(coinprefab, position);


        //void bossdefeated(){ }
        // Spawncoin
    }
}