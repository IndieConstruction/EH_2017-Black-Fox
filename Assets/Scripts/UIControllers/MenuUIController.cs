using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BlackFox
{
    public class MenuUIController : MonoBehaviour
    {
        public Image Backgorund;
        public Text text1;
        public Text text2;
        public Text text3;
        public Text text4;
        public Text text5;
        public Button button;

        UIManager managerUI;

        private void Start()
        {
            if (GameManager.Instance != null)
                managerUI = GameManager.Instance.GetUIManager();

            if (managerUI != null)
                managerUI.SetMenuUIController(this);
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}