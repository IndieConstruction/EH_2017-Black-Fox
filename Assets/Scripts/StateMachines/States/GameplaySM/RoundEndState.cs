using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlackFox
{
    public class RoundEndState : StateBase
    {
        EndRoundlUI EndLevelCanvas;

        public override void OnStart()
        {
            Debug.Log("RoundEndState");
            EventManager.OnRoundEnd();
            EndLevelCanvas = GameObject.FindObjectOfType<EndRoundlUI>();
            EndLevelCanvas.EndLevelPanel.SetActive(true);
            EndLevelCanvas.OnClickToChangeState += OnChangeState;
        }

        public override void OnUpdate()
        {
            
        }

        public override void OnEnd()
        {
            EndLevelCanvas.EndLevelPanel.SetActive(false);
            EndLevelCanvas.ClearTheUIPoints();
            EndLevelCanvas.OnClickToChangeState -= OnChangeState;
        }

        /// <summary>
        /// Richiama l'evento di fine stato per passare al successivo
        /// </summary>
        void OnChangeState()
        {
            if (OnStateEnd != null)
            {
                OnStateEnd(); 
            }
        }
    }
}
