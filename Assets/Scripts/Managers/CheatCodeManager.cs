using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class CheatCodeManager : MonoBehaviour
    {
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                GameManager.Instance.LevelMng.PlayerWin("CheatCode");
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {

            }
        }
    }
}