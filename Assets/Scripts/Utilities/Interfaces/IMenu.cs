using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public interface IMenu
    {

        int CurrentIndexSelection { get; set; }

        int TotalIndexSelection { get; set; }
        
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
            if (_this.CurrentIndexSelection > _this.TotalIndexSelection)
                _this.CurrentIndexSelection = 1;
        }

        /// <summary>
        /// Sposta l'indice della selezione indietro
        /// </summary>
        /// <param name="_this"></param>
        public static void GoUpInMenu(this IMenu _this)
        {
            _this.CurrentIndexSelection--;
            if (_this.CurrentIndexSelection < 1)
                _this.CurrentIndexSelection = _this.TotalIndexSelection;
        }

    }
}