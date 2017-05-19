using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox {
    public class LoadingScreen : MonoBehaviour
    {
        public Image LoadingPanel;
        public Image LoadingCricle;
        public float FadeRate;

        float LoadingPanelAlpha;

        private void Start()
        {
            LoadingPanelAlpha = LoadingPanel.color.a;
            LoadingPanelAlpha = 0f;
        }

        private void Update()
        {
            if (LoadingPanelAlpha > 0f)
                LoadingCricle.rectTransform.Rotate(Vector3.forward * Time.deltaTime); 
        }

        public void FadeIn()
        {
            LoadingPanelAlpha = Mathf.Lerp(LoadingPanelAlpha, 1f, FadeRate * Time.deltaTime);
        }

        public void FadeOut()
        {
            LoadingPanelAlpha = Mathf.Lerp(LoadingPanelAlpha, 0f, FadeRate * Time.deltaTime);
        }
    }
}