using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {

    public class GameOverState : StateBase {

        int levelNumber;

        public GameOverState(int _levelNumber)
        {
            levelNumber = _levelNumber;
        }

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

        void UnloadArena()
        {
            GameObject.Destroy(GameObject.Find("Level" + levelNumber  + "(Clone)"));
        }
    }
}
