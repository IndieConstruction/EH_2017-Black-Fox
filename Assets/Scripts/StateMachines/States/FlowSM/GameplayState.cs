using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlackFox
{
    /// <summary>
    /// Reppresenta lo stato di gameplay della flow state machine.
    /// </summary>
    public class GameplayState : StateBase
    {
        int levelNumber = 1;

        public override void OnStart()
        {
            Debug.Log("GameplayState");
            GameManager.Instance.InstantiateLevelManager();
            StateMachineBase.OnMachineEnd += OnMachineEnd;
        }

        public override void OnEnd()
        {
            StateMachineBase.OnMachineEnd -= OnMachineEnd;
        }

        void OnMachineEnd(string _machineName)
        {
            if(_machineName == "GameplaySM")
            {
                Debug.Log("GameplaySM_Stop");
                if (OnStateEnd != null)
                    OnStateEnd();
            }   
        }
    }
}
