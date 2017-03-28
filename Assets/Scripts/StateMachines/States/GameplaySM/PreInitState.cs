using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PreInitState : StateBase
    {
        int levelNumber;
        int roundNumber;

        public PreInitState(int _levelNumber, int _roundNumber)
        {
            levelNumber = _levelNumber;
            roundNumber = _roundNumber;
        }

        public override void OnStart()
        {
            Debug.Log("PreInitState");
            LoadArena();
            
        }

        public override void OnUpdate()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }

        void LoadManager()
        {
            SpawnerManager spawnerManager = GameManager.Instance.levelManager.InstantiateSpawnerManager();
            spawnerManager.Init(levelNumber, roundNumber);
            GameManager.Instance.levelManager.InstantiateRopeManager();
        }

        /// <summary>
        /// Istanzia il prefab del livello
        /// </summary>
        void LoadArena()
        {
            //GameObject.Instantiate(Resources.Load("Prefabs/Levels/Level" + levelNumber));
            GameObject.Instantiate(Resources.Load("Prefabs/Levels/Level1withRopes"), GameManager.Instance.levelManager.transform);
        }
    }
}