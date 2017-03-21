using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public delegate void GamePlayEvent();

    public static GamePlayEvent OnLevelInit;
    public static GamePlayEvent OnLevelPlay;
    public static GamePlayEvent OnLevelEnd;


}
