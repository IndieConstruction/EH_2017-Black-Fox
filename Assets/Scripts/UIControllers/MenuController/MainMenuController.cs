using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class MainMenuController : MonoBehaviour, IMenu
    {
        /// <summary>
        /// Il "bottone" selezionato
        /// </summary>
        int currentInexSelection = 1;

        public Text PlayText;
        public Text CreditsText;
        public Text ExitText;

        /// <summary>
        /// Il totale delle possibile scenlte
        /// </summary>
        int totalIndexSelection = 3;

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
        
        public int TotalIndexSelection
        {
            get
            {
                return totalIndexSelection;
            }
            set
            {
                totalIndexSelection = value;
            }
        }

        // Use this for initialization
        void Start()
        {
            UpdateGraphic();
            GameManager.Instance.UiMng.CurrentMenu = this;
        }

        /// <summary>
        /// Chiama la funzione per cambiare lo stato della StateMachine
        /// </summary>
        public void Selection()
        {
            switch (CurrentIndexSelection)
            {
                case 1:
                    // Cambia stato in level selection
                    GameManager.Instance.flowSM.GoToState(FlowSMStates.LevelSelectionState);
                    break;
                case 2:
                    // Apre i credits
                    break;
                case 3:
                    GameManager.Instance.QuitApplication();
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
            switch (CurrentIndexSelection)
            {
                case 1:
                    PlayText.color = Color.red;
                    CreditsText.color = Color.white;
                    ExitText.color = Color.white;
                    break;
                case 2:
                    PlayText.color = Color.white;
                    CreditsText.color = Color.red;
                    ExitText.color = Color.white;
                    break;
                case 3:
                    PlayText.color = Color.white;
                    CreditsText.color = Color.white;
                    ExitText.color = Color.red;
                    break;
                default:
                    break;
            }
        }

    }
}