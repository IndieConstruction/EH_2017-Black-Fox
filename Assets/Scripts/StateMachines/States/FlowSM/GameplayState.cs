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
        GameplaySM gameplaySM;
        UnityEngine.Object canvasGame;

        public override void OnStart()
        {
            Debug.Log("Gameplay");
            canvasGame = GameObject.Instantiate(Resources.Load("Prefabs/Misc/CanvasGame"));
            StartGameplaySM();       
        }

        public override void OnEnd()
        {
            StateMachineBase.OnMachineEnd -= OnMachineEnd;
            GameObject.Destroy(canvasGame);
        }

        void StartGameplaySM()
        {
            gameplaySM = GameManager.Instance.gameObject.AddComponent<GameplaySM>();
            StateMachineBase.OnMachineEnd += OnMachineEnd;
        }

        void OnMachineEnd(string _machineName)
        {
            if(_machineName == "GameplaySM")
            {
                Debug.Log("GameplaySM_Stop");
                GameObject.Destroy(gameplaySM);
                if (OnStateEnd != null)
                    OnStateEnd();
            }   
        }
    }
}
