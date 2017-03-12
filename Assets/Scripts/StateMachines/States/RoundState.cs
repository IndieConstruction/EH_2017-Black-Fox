using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox
{
    public class RoundState : StateBase
    {
        public enum RoundStates
        {
            Initialize,
            Play,
            Pause,
            RoundEnd,
        }

        private RoundStates currentState;
        /// <summary>
        /// Stato attuale.
        /// </summary>
        public RoundStates CurrentState
        {
            get { return currentState; }
            set
            {
                if (currentState != value)
                    onStateChanged(currentState, value);
                currentState = value;
            }
        }

        /// <summary>
        /// Accade ogni volta che cambia stato.
        /// </summary>
        void onStateChanged(RoundStates _oldState, RoundStates _newState)
        {
            switch (_newState)
            {
                case RoundStates.Initialize:
                    Debug.Log("Initialize");
                    break;
                case RoundStates.Play:
                    Debug.Log("Play");
                    break;
                case RoundStates.Pause:
                    Debug.Log("Pause");
                    break;
                case RoundStates.RoundEnd:
                    Debug.Log("RoundEnd");
                    break;
                default:
                    break;
            }
        }

        public override void OnStart()
        {
            Debug.Log("RoundState");
            currentState = RoundStates.Initialize;
        }

        public override void OnUpdate()
        {
            StateFlow();         
        }

        /// <summary>
        /// Flow della state machine
        /// </summary>
        void StateFlow()
        {
            switch (CurrentState)
            {
                case RoundStates.Initialize:
                    Initialize();
                    break;
                case RoundStates.Play:
                    Play();
                    break;
                case RoundStates.Pause:
                    Pause();
                    break;
                case RoundStates.RoundEnd:
                    RoundEnd();
                    break;
                default:
                    break;
            }      
        }

        void Initialize()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {               
                CurrentState = RoundStates.Play;
            }
        }

        void Play()
        {         
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentState = RoundStates.Pause;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentState = RoundStates.RoundEnd;
            }
        }

        void Pause()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CurrentState = RoundStates.Play;
            }
        }

        void RoundEnd()
        {
            if (OnStateEnd != null)
                OnStateEnd("RoundState");
        }
    }
}
