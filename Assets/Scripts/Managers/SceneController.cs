using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackFox
{
    public class SceneController : MonoBehaviour
    {
        #region API

        public void ReloadCurrentRound()
        {
            int SceneNumber = SceneManager.GetActiveScene().buildIndex;
            LoadScene(SceneNumber);
        }

        public void LoadScene(int _number)
        {
            SceneManager.LoadScene(_number);
        }

        #endregion
    }
}
