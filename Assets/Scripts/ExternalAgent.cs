using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class ExternalAgent : MonoBehaviour, IDamageable {

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    #region Interface

    public void Damage(float _damage, PlayerIndex _attacker)
    {
        throw new NotImplementedException();
    }

    #endregion
}
