using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace BlackFox
{
    public class LevelSelectionController : MonoBehaviour, IMenu
    {

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

        /// <summary>
        /// Il "bottone" selezionato
        /// </summary>
        int currentIndexSelection = 0;

        public int CurrentIndexSelection
        {
            get
            {
                return currentIndexSelection;
            }
            set
            {
                // Modifiche grafiche per cambiare colore alla nuova selezione e far tornare la vecchia selezione al colore precedente.
                currentIndexSelection = value;
                for (int i = 0; i < selectableButton.Count; i++)
                {
                    if (selectableButton[i].Index == value)
                    {
                        selectableButton[i].IsSelected = true;
                    }
                    else { selectableButton[i].IsSelected = false; }
                }
            }
        }

        /// <summary>
        /// Il totale delle possibile scenlte
        /// </summary>
        int totalIndexSelection = 2;

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
            OnActivation();
            GameManager.Instance.UiMng.CurrentMenu = this;
        }


        /// <summary>
        /// Salva all'interno della lista SelectableButton tutti i bottoni con attaccato ISelectable, gli assegna un index e chiama la funzioen che indica cosa scrivergli
        /// </summary>
        public void OnActivation()
        {
            foreach (ISelectable item in GetComponentsInChildren<ISelectable>())
            {
                SelectableButtons.Add(item);
            }

            for (int i = 0; i < selectableButton.Count; i++)
            {
                selectableButton[i].SetIndex(i);
            }

            selectableButton[0].IsSelected = true;
        }


        /// <summary>
        /// Chiama la funzione per cambiare lo stato della StateMachine
        /// </summary>
        public void Selection()
        {
            switch (CurrentIndexSelection)
            {
                case 0:
                    GameManager.Instance.flowSM.GoToState(FlowSMStates.GameplayState);
                    break;
                case 1:
                    GameManager.Instance.flowSM.GoToState(FlowSMStates.MainMenuState);
                    break;
                default:
                    break;
            }
        }
    }
}