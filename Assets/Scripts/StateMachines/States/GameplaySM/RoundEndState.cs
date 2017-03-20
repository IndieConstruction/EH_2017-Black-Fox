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
            foreach (var agent in GameObject.FindObjectsOfType<Agent>())
            {
                GameObject.Destroy(agent);
            }
        }

        void UnloadGameElements()
        {
            GameObject.Destroy(GameObject.FindObjectOfType<Core>());
        }

        void ClearArena()
        {
            GameObject.Destroy(GameObject.Find("SpawnpointOne"));
            GameObject.Destroy(GameObject.Find("SpawnpointTwo"));
            GameObject.Destroy(GameObject.Find("SpawnpointThree"));
            GameObject.Destroy(GameObject.Find("SpawnpointFour"));

            GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");

            foreach (GameObject pin in pins)
            {
                GameObject.Destroy(pin);
            }
        }
    }
}
