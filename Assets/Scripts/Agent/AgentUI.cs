using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox {

    public class AgentUI : MonoBehaviour {

        public Image Ring;
        Agent agent;
        public Text BulletCount;
        public Text KillPoint;
    
    // Use this for initialization
        void Awake() {
            agent = GetComponentInParent<Agent>();
            Ring.color = Color.green;
        }

        // Update is called once per frame
        void Update() {
            BulletCount.text = agent.SendInformation().ammo.ToString();
        }

        private void OnEnable() {
            agent.OnDataChange += OnDataChange;
            Agent.OnAgentKilled += SetKillPointUI;


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

        void SetKillPointUI(Agent _killer, Agent _victim)
        {
            if (_killer == agent)
            {
                //Aggiungi un punto alla UI.
                //KillPoint.text =
            }

            if (_victim == agent)
            {
                //Sottrai un punto alla UI.
            }
        }

        private void OnDisable() {
            agent.OnDataChange -= OnDataChange;
            Agent.OnAgentKilled -= SetKillPointUI;
        }
    }
}