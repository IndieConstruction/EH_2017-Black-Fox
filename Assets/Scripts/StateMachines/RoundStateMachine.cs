using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class RoundStateMachine : MonoBehaviour
    {
        private RoundState currentState;

        public RoundState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }

        private void Update()
        {
            switch (CurrentState)
            {
                case RoundState.Initialize:
                    break;
                case RoundState.Play:
                    break;
                case RoundState.Pause:
                    break;
                case RoundState.RoundEnd:
                    break;
                default:
                    break;
            }
        }

        public enum RoundState
        {
            Initialize,
            Play,
            Pause,
            RoundEnd
        }
    }
}
