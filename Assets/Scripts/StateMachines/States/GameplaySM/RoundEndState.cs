using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class RoundEndState : StateBase
    {
        int roundNumber;

        public RoundEndState(int _roundNumber)
        {
            roundNumber = _roundNumber;
        }

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
