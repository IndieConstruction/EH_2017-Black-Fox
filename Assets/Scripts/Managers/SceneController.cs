using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public float WaitForSeconds = 3;                // Il tempo che aspetta prima di riavviare la scena

    #region API

    public void ReloadCurrentRound()
    {  
        string SceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneName,LoadSceneMode.Single);

        //Assegna agli Avatar la vita che avevano all'inizion del round che hanno appena perso.
    }

    public void LoadScene(int _number)
    {
        StartCoroutine(ReStart(_number));
    }

    IEnumerator ReStart(int _number)
    {
        yield return new WaitForSeconds(WaitForSeconds);
        SceneManager.LoadScene(_number);
    }

    #endregion

}
