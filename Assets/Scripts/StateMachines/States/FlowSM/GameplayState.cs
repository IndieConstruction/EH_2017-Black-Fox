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
        UnityEngine.Object canvasGame;

        public override void OnStart()
        {
            Debug.Log("Gameplay");
            canvasGame = GameObject.Instantiate(Resources.Load("Prefabs/Misc/CanvasGame"));
            StateMachineBase.OnMachineEnd += OnMachineEnd;
            GameObject.Instantiate(Resources.Load("Prefabs/Misc/LevelManager"));
        }

        public override void OnEnd()
        {
            StateMachineBase.OnMachineEnd -= OnMachineEnd;
            GameObject.Destroy(canvasGame);
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
