using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class ArrowManager : MonoBehaviour
    {

        //public bool 
        Arrow[] arrows;
        int RandomSetup = 1;
        int maxRandomSetup = 0;

        private void Awake()
        {
            arrows = FindObjectsOfType<Arrow>();

            for (int i = 0; i < arrows.Length; i++)
            {
                if (maxRandomSetup < arrows[i].IDArrow)
                {
                    maxRandomSetup = arrows[i].IDArrow;
                }
            }

            RandomSetup = (int)Random.Range(1f, 4f);
            ChoseWhoActive();

        }

        void ChoseWhoActive()
        {

            for (int i = 0; i < arrows.Length; i++)
            {
                if (arrows[i].IDArrow != RandomSetup)
                {
                    arrows[i].gameObject.SetActive(false);
                }
            }

            /*foreach (var item in arrows)
            {

                if (item.IDArrow == 1 && RandomSetup == 1)
                {
                    item.gameObject.SetActive(true);
                } else if (item.IDArrow == 2 && RandomSetup == 2)
                {
                    item.gameObject.SetActive(true);
                } else if (item.IDArrow == 3 && RandomSetup == 3)
                {
                    item.gameObject.SetActive(true);
                }
                else if (item.IDArrow == 4 && RandomSetup == 4)
                {
                    item.gameObject.SetActive(true);
                }
                else if (item.IDArrow == 5 && RandomSetup == 5)
                {
                    item.gameObject.SetActive(true);
                }
                else
                {
                    item.gameObject.SetActive(false);
                }
            }*/
        }
    }
}
