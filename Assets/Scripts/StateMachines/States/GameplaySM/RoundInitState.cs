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
            Debug.Log("RoundInit");
            LoadAgents();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        void LoadAgents()
        {
            GameObject[] agents = Resources.LoadAll<GameObject>("Prefabs/Agents");

            foreach (GameObject item in agents)
            {
                GameObject.Instantiate(item);
            }
        }
    }
}
