using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class LevelEndState : StateBase
    {
        // TODO : correggere il funzionamente della classe
        // Funzioni scritte in modo orribile solo per test

        public override void OnStart()
        {
            Debug.Log("LevelEndState");
            UnloadArena();
            UnloadAgents();
            UnloadGameElements();
            if (OnStateEnd != null)
                OnStateEnd("LevelEndState");
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd("LevelStartState");
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
