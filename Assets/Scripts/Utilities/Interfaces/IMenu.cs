using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public interface IMenu
    {

        int CurrentIndexSelection { get; set; }

        List<ISelectable> SelectableButtons { get; set; }

        /// <summary>
        /// Chiama la funzione per cambiare lo stato della StateMachine
        /// </summary>
        void Selection();

    }

    public static class IMenuExtension
    {



        /// <summary>
        /// Sposta l'indice della selezione in avanti
        /// </summary>
        /// <param name="_this"></param>
        public static void GoDownInMenu(this IMenu _this)
        {
            _this.CurrentIndexSelection++;
            if (_this.CurrentIndexSelection > _this.SelectableButtons.Count)
                _this.CurrentIndexSelection = 0;
        }

        /// <summary>
        /// Sposta l'indice della selezione indietro
        /// </summary>
        /// <param name="_this"></param>
        public static void GoUpInMenu(this IMenu _this)
        {
            _this.CurrentIndexSelection--;
            if (_this.CurrentIndexSelection < 0)
                _this.CurrentIndexSelection = _this.SelectableButtons.Count - 1;
        }

    }


    /// <summary>
    /// Interfaccia per tutti gli oggetti selezionabili.
    /// </summary>
    public interface ISelectable
    {
        int Index{ get; set; }
        bool IsSelected { get; set; }
        void SetIndex(int _index);
        void CheckIsSelected(bool _isSelected);
    }
}