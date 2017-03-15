using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class RoundState : StateBase
    {
        int roundNumber;
        bool playerWinning;

        public enum RoundStates
        {
            Initialize,
            Play,
            Pause,
            RoundEnd
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

        public RoundState(int _number)
        {
            roundNumber = _number;
        }

        /// <summary>
        /// Accade ogni volta che cambia stato.
        /// </summary>
        void onStateChanged(RoundStates _oldState, RoundStates _newState)
        {
            switch (_newState)
            {
                case RoundStates.Initialize:
                    break;
                case RoundStates.Play:
                    // TODO : count down inzio round
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
            LevelManager.OnPlayerWinnig += HandleOnPlayerWinnig;
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
            if(roundNumber == 1)
            {
                // inzzzzializzzzazzzzione spanwn manager e parametri round
            }
            else
            {
                // reset di tutti gli oggeti presenti nel round
            }
            
            CurrentState = RoundStates.Play;
        }

        void Play()
        {
            if (playerWinning)
            {
                Debug.Log("Agent Wins");
                CurrentState = RoundStates.RoundEnd;
            }
        }

        void Pause()
        {
            // TODO : aggiungere collgamento alla pausa
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CurrentState = RoundStates.Play;
            }
        }

        void RoundEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            LevelManager.OnPlayerWinnig -= HandleOnPlayerWinnig;
            if (OnStateEnd != null)
                OnStateEnd();
        }

        #region Events
        void HandleOnPlayerWinnig()
        {
            playerWinning = true;
        }
        #endregion
    }
}
