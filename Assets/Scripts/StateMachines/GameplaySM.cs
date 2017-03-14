using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class GameplaySM : StateMachineBase
    {
        int roundNumber;

        private void Start()
        {
            Debug.Log("StartGPSM");
            CurrentState = new LevelStartState();
        }

        void HandleOnStateEnd(string _stateName)
        {
            switch (_stateName)
            {
                case "LevelStartState":
                    roundNumber++;
                    CurrentState = new RoundState(roundNumber);
                    break;
                case "RoundState":
                    // TODO : aggiungere condizione di spareggio
                    if(roundNumber < 4)
                    {
                        roundNumber++;
                        CurrentState = new RoundState(roundNumber);
                    }   
                    else
                    {
                        CurrentState = new LevelEndState();
                    }                      
                    break;
                case "LevelEndState":
                    //EXIT POINT   
                    roundNumber = 0;
                    if (OnMachineEnd != null)
                        OnMachineEnd("GameplaySM");
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            if (CurrentState != null)
                CurrentState.OnUpdate();
        }

        #region Events
        private void OnEnable()
        {
            StateBase.OnStateEnd += HandleOnStateEnd;            
        }
        private void OnDisable()
        {
            StateBase.OnStateEnd -= HandleOnStateEnd;
        }
        #endregion
    }
}
