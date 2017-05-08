using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public abstract class BaseMenu : MonoBehaviour, IMenu
    {
        protected int currentIndexSelection = 0;

        public int CurrentIndexSelection
        {
            get { return currentIndexSelection; }
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

        protected List<ISelectable> selectableButton = new List<ISelectable>();

        public List<ISelectable> SelectableButtons
        {
            get { return selectableButton; }
            set { selectableButton = value; }
        }

        /// <summary>
        /// Salva all'interno della lista SelectableButton tutti i bottoni con attaccato ISelectable, gli assegna un index
        /// </summary>
        public virtual void FindISelectableChildren()
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
        /// Funzione che in base all'override esegue la funzione del bottone attualmente selezionato
        /// </summary>
        public virtual void Selection() { }
    }
}