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
    }

    #endregion

}
