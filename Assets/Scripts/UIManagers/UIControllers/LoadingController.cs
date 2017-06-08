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
                panelImg.DOFade(1, .3f).OnComplete(_action);
            else
                panelImg.DOFade(1, 0.3f);
        }

        public void DeactivateLoadingPanel(TweenCallback _action = null) {
            if (_action != null)
                panelImg.DOFade(0, .3f).OnComplete(_action);
            else
                panelImg.DOFade(0, .3f);
        }

        #endregion


    }
}