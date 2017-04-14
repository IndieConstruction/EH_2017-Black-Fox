using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{


    private static int _coins = 0;

    public static int Coins
    {
        get { return _coins; }
        set { _coins = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("coins"))
        {
            Destroy(other.gameObject);
            Coins++;
        }
    }

    //void Spawncoin(GameObject coinprefab, Transform.position){ }

    //Instantiate(coinprefab, position);


    //void bossdefeated(){ }
    // Spawncoin
}



