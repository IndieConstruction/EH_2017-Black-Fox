using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class CheatCodeManager : MonoBehaviour
    {
        public GameObject CheatPanel;
        InputField inputField;

        private void Start()
        {
            inputField = GetComponentInChildren<InputField>();
            CheatPanel.SetActive(false);
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                ActiveInputField();
            }
        }

        /// <summary>
        /// Azione da compiere dopo aver inserito una stringa nell'input field
        /// </summary>
        /// <param name="_cheat"></param>
        public void GetInput(string _cheat)
        {
            switch (_cheat)
            {
                case "ammo":
                    foreach (Player player in GameManager.Instance.PlayerMng.Players)
                    {
                        player.Avatar.ship.Shooter.MaxAmmo = 500;
                        player.Avatar.ship.Shooter.ammo = 500;
                    }
                    Debug.Log("Infinite Ammo");
                    break;
                case "round":
                    GameManager.Instance.LevelMng.PlayerWin("CheatCode");
                    break;
                case "level":
                    GameManager.Instance.LevelMng.gameplaySM.GoToState(GamePlaySMStates.GameOverState);
                    break;

                default:
                    Debug.LogWarning("Wrong CheatCode");
                    break;
            }
            inputField.text = "";
            CheatPanel.SetActive(false);
        }

        /// <summary>
        /// Attiva e seleziona la casella di testo dell'input field
        /// </summary>
        void ActiveInputField()
        {
            CheatPanel.SetActive(true);
            inputField.Select();
            inputField.ActivateInputField();
        }
    }
}