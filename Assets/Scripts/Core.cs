using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable {

    //public float Life;
    
    #region Interfacce

    public void Damage(float _damage)
    {
        GameManager.Instance.CoreLife -= _damage;
        FindObjectOfType<UIManager>().SetSliderValue(GameManager.Instance.CoreLife);
        if (GameManager.Instance.CoreLife < 1) {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine("ReStart");
        }
    }


    

    void PassoValoreVita()
    {
        // Passo il valore della mia vita al GameManager
    }

  

    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(GameManager.Instance.WaitForSeconds);
        GameManager.Instance.coreLife = GameManager.Instance.MaxLifeCore;
        GameManager.Instance.sceneController.ReloadCurrentRound();
    }

    /// <summary>
    /// Carica il round successivo, passando la vita del core al GameManager
    /// </summary>
    /// <returns></returns>
    IEnumerator CaricaNextRound()
    {
        yield return new WaitForSeconds(3);
        //Salva la vita del core dentro la variabile CoreLife presente nel GameManager
        //Carica il Round/Livello successivo.
    }


    /// <summary>
    /// Appena viene caricata la scena il GameManager passa al Core quanta vita si deve settare 
    /// </summary>
    void CaricoLaVitaDelGameManager()
    {
        //Prende la CoreLife del GameManager e la passa alla vita del Core
    }

    #endregion
}