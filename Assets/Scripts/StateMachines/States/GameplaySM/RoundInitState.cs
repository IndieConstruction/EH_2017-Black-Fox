using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class RoundInitState : StateBase {

        int roundNumber;

        public RoundInitState(int _roundNumber)
        {
            roundNumber = _roundNumber;
        }

        public override void OnStart()
        {
            Debug.Log("RoundInitState");
            if (roundNumber > 1)
            {
                ReloadAgents();
                ReloadGameElements();
            }
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        void ReloadAgents()
        {
            GameObject[] agents = Resources.LoadAll<GameObject>("Prefabs/Agents");

            foreach (GameObject item in agents)
            {
                GameObject.Instantiate(item);
            }
        }

        void ReloadGameElements()
        {
            GameObject.Instantiate(Resources.Load("Prefabs/Misc/Core"));
        }
    }
}
