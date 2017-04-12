using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{

    public class PauseMenuController : MonoBehaviour, IMenu
    {

        
        public GameObject ChildrenPanel;

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



        private void Start()
        {
            ChildrenPanel.SetActive(false);
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

        public void Selection()
        {
            switch (CurrentIndexSelection)
            {
                case 0:
                    GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
                    break;
                case 1:
                    GameManager.Instance.LevelMng.gameplaySM.GoToState(GamePlaySMStates.GameOverState);
                    break;
            }
        }
    }
}