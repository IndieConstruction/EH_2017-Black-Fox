using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class CoinManager : MonoBehaviour
    {

        private int _coinsCollected = 0;
        public int CurrentCoins; 

        public int CoinsCollected 
        {
            get { return _coinsCollected; }
            set { _coinsCollected = value; }
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