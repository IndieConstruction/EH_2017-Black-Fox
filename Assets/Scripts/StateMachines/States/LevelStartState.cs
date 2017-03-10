using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    /// <summary>
    /// Costruisce la scena a runtime
    /// </summary>
    public class LevelStartState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("LevelStart");
            LoadArena();
            LoadAgents();
            LoadGameElements();
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (OnStateEnd != null)
                    OnStateEnd("LevelStartState");
            }

        }

        /// <summary>
        /// Istanzia componenti dell'arena
        /// </summary>
        void LoadArena()
        {
            GameObject.Instantiate(Resources.Load("Prefabs/Misc/Walls"));         
        }

        /// <summary>
        /// Istanzia Agenti
        /// </summary>
        void LoadAgents()
        {
            GameObject[] agents = Resources.LoadAll<GameObject>("Prefabs/Agents");

            foreach (GameObject item in agents)
            {
                GameObject.Instantiate(item);
            }
        }

        /// <summary>
        /// Istanzia Elementi di gioco
        /// </summary>
        void LoadGameElements()
        {
            GameObject.Instantiate(Resources.Load("Prefabs/Misc/Core"));
        }
    }
}
