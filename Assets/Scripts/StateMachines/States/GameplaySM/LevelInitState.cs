using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class LevelInitState : StateBase
    {
        // TODO : in questo stato si dovrebbe ascoltare l'evento OnAgentSpawn
        // TODO : fare in modo che lo stato non finisca prima di aver inizializzato tutto quello che c'è in scena

        public override void OnStart()
        {
            Debug.Log("LevelInitState");
            GameManager.Instance.uiManager.UpdateLevelInformation();
            GameManager.Instance.levelManager.InitCore();
            GameManager.Instance.levelManager.CallSpawnAgent();

        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}