using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{

    public class PauseMenuController : MonoBehaviour, IMenu
    {

        public Text ResumeText;
        public Text MainMenuText;

        public GameObject ChildrenPanel;

        int currentInexSelection = 1;

        public int CurrentIndexSelection
        {
            get
            {
                return currentInexSelection;
            }
            set
            {
                /// Modifiche grafiche per cambiare colore alla nuova selezione e far tornare la vecchia selezione al colore precedente.
                currentInexSelection = value;
                UpdateGraphic();
            }
        }
        List<ISelectable> selectableButton = new List<ISelectable>();

        public List<ISelectable> SelectableButtons
        {
            get
            {
                return selectableButton;
            }

            set
            {
                selectableButton = value;
            }
        }


        private void Start()
        {
            ChildrenPanel.SetActive(false);
        }



        public void Selection()
        {
            switch (CurrentIndexSelection)
            {
                case 1:
                    // Cambia stato in level selection
                    GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
                    break;
                case 2:
                    //TODO: scommentare in seguito
                    //GameManager.Instance.LevelMng.gameplaySM.GoToState(GamePlaySMStates.GameOverState);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Aggiorna la grafica dei bottoni
        /// </summary>
        void UpdateGraphic()
        {
            switch (currentInexSelection)
            {
                case 1:
                    ResumeText.color = Color.red;
                    MainMenuText.color = Color.white;
                    break;
                case 2:
                    ResumeText.color = Color.white;
                    MainMenuText.color = Color.red;
                    break;
                default:
                    ResumeText.color = Color.white;
                    MainMenuText.color = Color.white;
                    break;
            }
        }
        
    }
}