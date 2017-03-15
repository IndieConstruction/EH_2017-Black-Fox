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

        protected override void OnCurrentStateEnded() {
            if ("BlackFox.LevelStartState" == CurrentState.StateName) {
                // LevelStartState
                roundNumber++;
                CurrentState = new RoundState(roundNumber);
            } else if ("BlackFox.RoundState" == CurrentState.StateName) {
                // RoundState
                // TODO : aggiungere condizione di spareggio
                if (roundNumber < 4) {
                    roundNumber++;
                    CurrentState = new RoundState(roundNumber);
                } else {
                    CurrentState = new LevelEndState();
                }
            } else if ("BlackFox.LevelEndState" == CurrentState.StateName) {
                // LevelEndState
                //EXIT POINT   
                roundNumber = 0;
                if (OnMachineEnd != null)
                    OnMachineEnd("GameplaySM");
            }
        }
    }
}
