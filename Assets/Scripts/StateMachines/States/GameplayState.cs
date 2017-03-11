﻿using System;
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
        bool IsGameplaySMActive;
        UnityEngine.Object canvasGame;

        public override void OnStart()
        {
            Debug.Log("Gameplay");
            canvasGame = GameObject.Instantiate(Resources.Load("Prefabs/Misc/CanvasGame"));
            StartGameplaySM();       
        }

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !IsGameplaySMActive)
            {
                if (OnStateEnd != null)
                    OnStateEnd("GameplayState");
            }
        }

        public override void OnEnd()
        {
            StateMachineBase.OnMachineEnd -= OnMachineEnd;
            GameObject.Destroy(canvasGame);
        }

        void StartGameplaySM()
        {
            gameplaySM = new GameObject().AddComponent<GameplaySM>();
            gameplaySM.gameObject.name = "GameplayStateMachine";
            IsGameplaySMActive = true;
            StateMachineBase.OnMachineEnd += OnMachineEnd;
        }

        void OnMachineEnd(string _machineName)
        {
            if(_machineName == "GameplaySM")
            {
                Debug.Log("GameplaySM_Stop");
                IsGameplaySMActive = false;
                GameObject.Destroy(gameplaySM.gameObject);
            }   
        }
    }
}
