using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {



    #region API

    public void ReloadCurrentRound()
    {
        
        string SceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(SceneName,LoadSceneMode.Single);

        //Assegna agli Avatar la vita che avevano all'inizion del round che hanno appena perso.

    }

    public void LoadScene(int _number)
    {
        SceneManager.LoadScene(_number);
    }

    #endregion

}
