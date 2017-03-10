using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BlackFox {
    /// <summary>
    /// State machine che gestisce il flow generale dell'applicazione.
    /// </summary>
    public class FlowSM : StateMachineBase {

        private void Start()
        {
            CurrentState = new MainMenuState();
        }

        void OnStateEnd(string _stateName)
        {
            switch (_stateName) {
                case "MainMenuState":
                    CurrentState = new GameplayState();
                    break;
                case "GameplayState":
                    CurrentState = new MainMenuState();
                    break;
                default:
                    break;
            }
        }

        #region Events
        private void OnEnable()
        {
            StateBase.OnStateEnd += OnStateEnd;
            AddListenerToButton();
        }
        private void OnDisable()
        {
            StateBase.OnStateEnd -= OnStateEnd;
            if (GameManager.Instance.TestSceneButton)
                GameManager.Instance.TestSceneButton.onClick.RemoveAllListeners();
        }

        // TODO : gli eventi non vengono richiamati al click del bottone

        private void OnLevelWasLoaded(int level)
        {
            AddListenerToButton();
        }

        void AddListenerToButton()
        {
            if (GameManager.Instance.TestSceneButton)
            {
                GameManager.Instance.TestSceneButton.onClick.RemoveAllListeners();
                GameManager.Instance.TestSceneButton.onClick.AddListener(() => {
                    CurrentState = new GameplayState();
                });
            }
        }
        #endregion
    }
}
