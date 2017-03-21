using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class EventManager : MonoBehaviour
    {

        public delegate void GamePlayEvent();

        public static GamePlayEvent OnLevelInit;
        public static GamePlayEvent OnLevelPlay;
        public static GamePlayEvent OnLevelEnd;

    }
}
