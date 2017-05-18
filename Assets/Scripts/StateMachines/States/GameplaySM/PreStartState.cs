using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PreStartState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PreStartState");
            //GameManager.Instance.UiMng.loadingCanvas.FadeOut();
            //GameManager.Instance.UiMng.DestroyLoadingCanvas();

            GameManager.Instance.UiMng.canvasGame.Counter.DoCountDown();
        }
    }
}