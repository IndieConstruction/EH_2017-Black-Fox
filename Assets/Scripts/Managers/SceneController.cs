using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    public float WaitForSeconds = 3;                // Il tempo che aspetta prima di riavviare la scena

    #region API

    public void ReloadCurrentRound()
    {  
        int SceneNumber = SceneManager.GetActiveScene().buildIndex;
        LoadScene(SceneNumber);
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
