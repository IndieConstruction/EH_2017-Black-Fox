using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class SelectableButton : MonoBehaviour, ISelectable
    {
        int index;

        bool isSelected;

        public bool IsSelected {
            get { return isSelected; }
            set { isSelected = value;
                CheckIsSelected(isSelected);
                }
        }

        public int Index
        {
            get { return index;}

            set { index = value; }
        }

        Text LabelText;

        public void SetIndex(int _index)
        {  
            Index = _index;
        }
        

        public void CheckIsSelected(bool _isSelected)
        {
            LabelText = GetComponentInChildren<Text>();

            if (_isSelected)
                LabelText.color = Color.red; 
            else
                LabelText.color = Color.white;
        }
    }
}