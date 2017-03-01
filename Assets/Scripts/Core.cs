using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable {

    float life;
    public float MaxLife = 10;              // La vita massima che può avere il Core e che viene impostata al riavvio di un round perso
    GameManager gameManager;

    public float Life
    {
        get { return life; }
        set { life = value; }
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        if (gameManager.CoreLife == 0)
            life = MaxLife;
        else
            life = gameManager.CoreLife;

        if (gameManager.GetGameUIController() != null)
            gameManager.CoreSliderValueUpdate(life);
    }

    #region Interfacce

    public void Damage(float _damage, GameObject _attacker)
    {
        life -= _damage;
        if (gameManager.GetGameUIController() != null)
            gameManager.CoreSliderValueUpdate(life);
        if (life < 1)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            gameManager.ReloadScene();
        }
    }

    #endregion
}