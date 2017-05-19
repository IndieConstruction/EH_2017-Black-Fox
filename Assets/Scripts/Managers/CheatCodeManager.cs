using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class CheatCodeManager : MonoBehaviour
    {
        public GameObject CheatPanel;
        InputField inputField;

        char delimiter = '#';

        private void Start()
        {
            inputField = GetComponentInChildren<InputField>();
            inputField.DeactivateInputField();
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
            if (_cheat == "ammo")
            {
                foreach (Player player in GameManager.Instance.PlayerMng.Players)
                {
                    player.Avatar.ship.shooter.AmmoCheat();
                }
                Debug.Log("Infinite Ammo");
            }
            else if (_cheat == "round")
                GameManager.Instance.LevelMng.PlayerWin("CheatCode");
            else if (_cheat == "level")
                GameManager.Instance.LevelMng.gameplaySM.SetPassThroughOrder(new List<StateBase>() { new CleanSceneState(), new GameOverState() });
            else if (_cheat.Contains("bull"))
            {
                string[] subStrings = _cheat.Split(delimiter);
                int index = Int32.Parse(subStrings[1]);
                GameManager.Instance.PlayerMng.Players[index].AvatarData = Instantiate(Resources.Load("ShipModels/Bull") as AvatarData);
            }
            else if(_cheat.Contains("bird"))
            {
                string[] subStrings = _cheat.Split(delimiter);
                int index = Int32.Parse(subStrings[1]);
                GameManager.Instance.PlayerMng.Players[index].AvatarData = Instantiate(Resources.Load("ShipModels/Hummingbird") as AvatarData);
            }
            else if(_cheat.Contains("shark"))
            {
                string[] subStrings = _cheat.Split(delimiter);
                int index = Int32.Parse(subStrings[1]);
                GameManager.Instance.PlayerMng.Players[index].AvatarData = Instantiate(Resources.Load("ShipModels/Shark") as AvatarData);
            }
            else if(_cheat.Contains("owl"))
            {
                string[] subStrings = _cheat.Split(delimiter);
                int index = Int32.Parse(subStrings[1]);
                GameManager.Instance.PlayerMng.Players[index].AvatarData = Instantiate(Resources.Load("ShipModels/Owl") as AvatarData);
            }
            else
                Debug.LogWarning("Wrong CheatCode");

            inputField.text = "";
            inputField.DeactivateInputField();
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