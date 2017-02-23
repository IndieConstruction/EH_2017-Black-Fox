using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : MonoBehaviour, IDamageable {

    #region Interfacce

    public void Damage(float _damage)
    {
        GameManager.Instance.CoreLife -= _damage;
        GetComponent<UIDisplay>().SetSliderValue(GameManager.Instance.CoreLife);
        if (GameManager.Instance.CoreLife < 1) {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            StartCoroutine("ReStart");
        }
    }

    IEnumerator ReStart()
    {
        yield return new WaitForSeconds(3);
        GameManager.Instance.sceneController.ReloadCurrentRound();
    }

    #endregion
}
