using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class LoadGameState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("LoadGameState");
            GameManager.Instance.InstantiateAvatarManager();
            GameManager.Instance.InstantiateUIManager();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}