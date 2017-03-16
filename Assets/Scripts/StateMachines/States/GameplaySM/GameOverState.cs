using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class GameOverState : StateBase {

        public override void OnStart()
        {
            Debug.Log("GameOverState");
            UnloadArena();
            UnloadAgents();
            UnloadGameElements();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        void UnloadAgents()
        {
            GameObject.Destroy(GameObject.Find("AgentBlue(Clone)"));
            GameObject.Destroy(GameObject.Find("AgentRed(Clone)"));
            GameObject.Destroy(GameObject.Find("AgentGreen(Clone)"));
            GameObject.Destroy(GameObject.Find("AgentPurple(Clone)"));

            GameObject.Destroy(GameObject.Find("SpawnpointOne"));
            GameObject.Destroy(GameObject.Find("SpawnpointTwo"));
            GameObject.Destroy(GameObject.Find("SpawnpointThree"));
            GameObject.Destroy(GameObject.Find("SpawnpointFour"));
        }

        void UnloadArena()
        {
            GameObject.Destroy(GameObject.Find("Floor(Clone)"));
        }

        void UnloadGameElements()
        {
            GameObject.Destroy(GameObject.Find("Core(Clone)"));
            GameObject.Destroy(GameObject.Find("RoundController(Clone)"));
            GameObject.Destroy(GameObject.Find("LevelManager(Clone)"));
        }
    }
}
