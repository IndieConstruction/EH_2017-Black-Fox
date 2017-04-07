using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox {

    public class AvatarUI : MonoBehaviour {
        public Image KillToview;
        public Slider Ring;
        
        
        Avatar agent;
        LevelManager levelManager;

        // Use this for initialization
        void Awake() {
            agent = GetComponentInParent<Avatar>();
        }

        private void Start()
        {
            levelManager = GameManager.Instance.LevelMng;
            Ring.value = 0.5f;
        }

        /// <summary>
        /// Setta il valore della slider che mostra la vita
        /// </summary>
        /// <param name="_agent"></param>
        void OnDataChange(Avatar _agent) {
            // Aggiorno la UI
            Ring.value =  (0.5f * _agent.Life) / _agent.MaxLife;

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
            agent.OnDataChange += OnDataChange;
        }

        private void OnDisable() {
            agent.OnDataChange -= OnDataChange;
        }
        #endregion

        //public void Killview()
        //{
        //    if(KillPoint != null)
        //        KillPoint.text="+1"+ (levelManager.GetPlayerKillPoints(agent.playerIndex)).ToString();

        //}


    }
}