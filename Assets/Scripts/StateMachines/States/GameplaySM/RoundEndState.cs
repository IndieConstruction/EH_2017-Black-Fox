using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BlackFox
{
    public class RoundEndState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("RoundEndState");
            GameManager.Instance.uiManager.endRoundUI.EndLevelPanel.SetActive(true);
            EventManager.OnClickToChangeState += OnChangeState;
        }

        public override void OnEnd()
        {
            GameManager.Instance.uiManager.endRoundUI.EndLevelPanel.SetActive(false);
            GameManager.Instance.uiManager.endRoundUI.ClearTheUIPoints();
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
