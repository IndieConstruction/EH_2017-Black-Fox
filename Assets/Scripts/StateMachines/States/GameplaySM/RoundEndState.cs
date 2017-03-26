using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlackFox
{
    public class RoundEndState : StateBase
    {

        public EndLevelUI OnEndLevelUI;


        public override void OnStart()
        {
            Debug.Log("RoundEndState");
            EventManager.OnRoundEnd();
            ClearArena();
        }

        public override void OnUpdate()
        {
            
        }



        void ClearArena()
        {
            ClearAgent();
            ClearExternalAgent();
            GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");

            foreach (GameObject pin in pins)
            {
                GameObject.Destroy(pin);
            }
        }

        void ClearAgent()
        {
            Agent[] agents = GameObject.FindObjectsOfType<Agent>();

            foreach (Agent agent in agents)
            {
                GameObject.Destroy(agent.gameObject);
            }
        }

        void ClearExternalAgent()
        {
            ExternalAgent[] agents = GameObject.FindObjectsOfType<ExternalAgent>();

            foreach (ExternalAgent extAgent in agents)
            {
                GameObject.Destroy(extAgent.gameObject);
            }
        }
    }
}
