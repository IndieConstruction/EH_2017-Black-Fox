using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{

    public class UpgradeMenuController : MonoBehaviour
    {
        public GameObject UpgradePanel;

        private void Start()
        {
            UpgradePanel.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                UpgradePanel.SetActive(true);
                OnStart();
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                UpgradePanel.SetActive(false);
            }
        }

        void OnStart()
        {
            foreach (PlayerUpgradeController controller in GetComponentsInChildren<PlayerUpgradeController>())
            {
                controller.OnStart();
                foreach (Player player in GameManager.Instance.PlayerMng.Players)
                {
                    if ((int)player.ID == (int)controller.MenuID)
                    {
                        controller.Avatar = player.Avatar;
                    }
                }

            }
        }
        
    }
}