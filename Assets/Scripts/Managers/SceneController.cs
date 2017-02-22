using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ReloadCurrentRound();
    }

    #region API

    public void ReloadCurrentRound()
    {
        
        string SceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneName,LoadSceneMode.Single);

        //Assegna agli Avatar la vita che avevano all'inizion del round che hanno appena perso.

    }

    public void OpenTestScene()
    {
        Debug.Log("openScene");
        SceneManager.LoadScene(1);
    }

    #endregion

}
