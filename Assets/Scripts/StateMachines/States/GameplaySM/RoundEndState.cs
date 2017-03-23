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
            EventManager.OnLevelEnd();
            ClearArena();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        void ClearAgent()
        {
            Agent[] agents = GameObject.FindObjectsOfType<Agent>();

            foreach (Agent agent in agents)
            {
                GameObject.Destroy(agent.gameObject);
            }
        }

        void ClearArena()
        {
            ClearAgent();
            GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");

            foreach (GameObject pin in pins)
            {
                GameObject.Destroy(pin);
            }
        }
    }
}
