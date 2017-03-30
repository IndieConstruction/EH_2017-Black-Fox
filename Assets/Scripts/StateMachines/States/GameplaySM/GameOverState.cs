using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class GameOverState : StateBase {

        public override void OnStart()
        {
            Debug.Log("GameOverState");
            UnloadArena();
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        /// <summary>
        /// Distrugge il level manager che ha come figlo l'intero livello
        /// </summary>
        void UnloadArena()
        {
            GameObject.Destroy(GameManager.Instance.levelManager.gameObject, 0.1f);
        }
    }
}
