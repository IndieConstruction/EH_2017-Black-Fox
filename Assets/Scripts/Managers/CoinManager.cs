using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{


    private static int _coins = 0;
    public static int CurrentCoins; //monete salvate

    public static int coins //monete acquisite ma non ancora salvate
    {
        get { return _coins; }
        set { _coins = value; }
    }

    void Awake()
    {
        CurrentCoins = PlayerPrefs.GetInt("CurrentCoins");
    }

    public static void AddCoins()
    {
        CurrentCoins = CurrentCoins + coins;
        PlayerPrefs.SetInt("CurrentCoins", CurrentCoins);
    }


    /// <summary>
    ///dove segue ho messo degli appunti per ricordare per quando si farà il boss e lo spawn in altri casi delle monete 
    /// </summary>


    //void Spawncoin(GameObject coinprefab, Transform.position){ }

    //Instantiate(coinprefab, position);


    //void bossdefeated(){ }
    // Spawncoin
}



