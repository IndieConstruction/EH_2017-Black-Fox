﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour {

    float life;
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

    public float Damage(float _damage)
    {
        Life -= _damage;
        if (Life < 1) {
            CoreIsAlive = false;

        }

        GameManager.Instance.CoreLife = Life;

        return Life;
    }

    #endregion
}