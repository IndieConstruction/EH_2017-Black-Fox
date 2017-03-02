using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementZero : MonoBehaviour, IDamageable {

    public float Life = 25;
    public float MaxLife = 50;
    Core core;
    public float DamageToCore = 1f;
    public float WhenDamage = 2;
    float TimeLeft;
    bool CanDamageCore;



    // Use this for initialization
    void Start()
    {
        core = FindObjectOfType<Core>();
        TimeLeft = WhenDamage;
        CanDamageCore = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CanDamageCore == true)
        {
            DamageCore();
        }

    }

    void ChargeLife()
    {
        Life = Life + 1 * Time.deltaTime;
    }

    void DamageCore()
    {
        TimeLeft -= Time.deltaTime;
        if (TimeLeft == 0)
        {
            core.Damage(DamageToCore, gameObject);
            TimeLeft = WhenDamage;
        }
    }

    public void Damage(float _damage, GameObject _attacker)
    {
        Life -= _damage;
        if (Life < 1)
        {
            CanDamageCore = true;
        }
    }

}