using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace BlackFox {
    public class LoadingController : MonoBehaviour {

        public Image panelImg;
        
        #region API

        public void ActivateLoadingPanel(TweenCallback _action = null) {
            if (_action != null)
                panelImg.DOFade(1, 2).OnComplete(_action);
            else
                panelImg.DOFade(1, 2);
        }

        public void DeactivateLoadingPanel(TweenCallback _action = null) {
            if (_action != null)
                panelImg.DOFade(0, 2).OnComplete(_action);
            else
                panelImg.DOFade(0, 2);
        }

        #endregion


    }
}