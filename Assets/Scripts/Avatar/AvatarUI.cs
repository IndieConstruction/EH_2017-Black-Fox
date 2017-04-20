using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace BlackFox {

    public class AvatarUI : MonoBehaviour {
        public Image KillToview;
        public Slider Ring;
              
        Ship ship;

        private void Start()
        {
            Ring.value = 0.5f;         
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.O))
            {
                KillView();
            }
        }
        /// <summary>
        /// Setta il valore della slider che mostra la vita
        /// </summary>
        /// <param name="_ship"></param>
        void OnDataChange(Ship _ship) {
            // Aggiorno la UI
            Ring.value =  (0.5f * _ship.Life) / _ship.MaxLife;

            //Logica per cambiare il colore della barra della vita
            //if (Ring.fillAmount < 0.3f) {
            //    Ring.color = Color.red;
            //} else if (Ring.fillAmount > 0.7f) {
            //    Ring.color = Color.green;
            //} else {
            //    Ring.color = Color.yellow;
            //}
                
        }

        #region Events

        private void OnEnable()
        {
            ship.OnDataChange += OnDataChange;
        }

        private void OnDisable() {
            ship.OnDataChange -= OnDataChange;
        }
        #endregion

        public void KillView() {
            KillToview.transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() => {
                KillToview.transform.localScale = Vector3.zero;
            }).SetEase(Ease.OutBounce);
        }
    }
}