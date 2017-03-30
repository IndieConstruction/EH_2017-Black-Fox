using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlackFox
{
    public class RoundEndState : StateBase
    {
        UIManager uiManager;

        public override void OnStart()
        {
            Debug.Log("RoundEndState");
            uiManager = GameObject.FindObjectOfType<UIManager>();
            uiManager.endRoundUI.EndLevelPanel.SetActive(true);
            EventManager.OnClickToChangeState += OnChangeState;
        }

        public override void OnEnd()
        {
            uiManager.endRoundUI.EndLevelPanel.SetActive(false);
            uiManager.endRoundUI.ClearTheUIPoints();
            EventManager.OnClickToChangeState -= OnChangeState;
        }

        /// <summary>
        /// Richiama l'evento di fine stato per passare al successivo
        /// </summary>
        void OnChangeState()
        {
            if (OnStateEnd != null)
                OnStateEnd();
        }
    }
}
