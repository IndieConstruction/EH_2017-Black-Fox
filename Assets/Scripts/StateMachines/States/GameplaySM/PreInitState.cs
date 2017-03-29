using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PreInitState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PreInitState");
            GameManager.Instance.levelManager.InstantiateArena();
            GameManager.Instance.levelManager.InstantiateRopeManager();
            GameManager.Instance.levelManager.InstantiateSpawnerManager();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}