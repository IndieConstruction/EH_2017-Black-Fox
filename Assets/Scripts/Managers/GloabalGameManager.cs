using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {
    public class GloabalGameManager : MonoBehaviour
    {
        static GloabalGameManager Instance;

        SceneController sceneController;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            // Singleton paradigm
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            
            sceneController = GetComponent<SceneController>();
        }
        void Start()
        {
            // stato iniziale di gioco
            //CurrentState = new MainMenuState();
        }

        void Update()
        {

        }

        public void ChangeScene(int _sceneNumber)
        {
            //CurrentState = new GameplayState();
            sceneController.LoadScene(_sceneNumber);
        }
    }
}
