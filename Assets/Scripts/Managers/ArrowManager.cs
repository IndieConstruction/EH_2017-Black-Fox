using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

    Arrow[] arrows;
    public int RandomSetup;

    private void Awake()
    {
        RandomSetup = (int)Random.Range(1f, 4f);
        arrows = FindObjectsOfType<Arrow>();
        ChoseWhoActive();
    }

    void ChoseWhoActive()
    {
        foreach (var item in arrows)
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
        }
    }
}
