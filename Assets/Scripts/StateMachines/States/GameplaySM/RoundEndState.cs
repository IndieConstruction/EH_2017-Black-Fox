using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class RoundEndState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("RoundEndState");
            UnloadAgents();
            UnloadGameElements();
            ClearArena();
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

        void UnloadGameElements()
        {
            GameObject.Destroy(GameObject.Find("Core(Clone)"));
        }

        void ClearArena()
        {
            GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");

            foreach (GameObject pin in pins)
            {
                GameObject.Destroy(pin);
            }
        }
    }
}
