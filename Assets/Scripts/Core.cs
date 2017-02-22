using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable {

    float life = 10;
    bool CoreIsAlive = true;

    #region API
    public float Life
    {
        get
        {
            return life;
        }
        set
        {
            life = value;
        }
    }

    #endregion

    #region Interfacce

    public void Damage(float _damage)
    {
        Life -= _damage;
        if (Life < 1) {
            CoreIsAlive = false;
            GameManager.Instance.sceneController.ReloadCurrentRound();          
        }
        GameManager.Instance.CoreLife = Life;
    }

    #endregion
}
