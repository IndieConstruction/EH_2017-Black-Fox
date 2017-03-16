using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox {

    public class AgentUI : MonoBehaviour {

        public Image Ring;
        Agent agent;
    
    // Use this for initialization
        void Awake() {
            agent = GetComponentInParent<Agent>();
            Ring.color = Color.green;
        }

        // Update is called once per frame
        void Update() {

        }

        private void OnEnable() {
            agent.OnDataChange += OnDataChange;
        }

        void OnDataChange(Agent _agent) {
            // Aggiorno la UI
            Ring.fillAmount =  _agent.Life / _agent.maxLife;
            if (Ring.fillAmount < 0.3f) {
                Ring.color = Color.red;
            } else if (Ring.fillAmount > 0.7f) {
                Ring.color = Color.green;
            } else {
                Ring.color = Color.yellow;
            }
                
        }

        private void OnDisable() {
            agent.OnDataChange -= OnDataChange;
        }
    }
}