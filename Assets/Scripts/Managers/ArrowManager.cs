using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour {

    Arrow[] arrows;
    public int RandomSetup = 1;
    private int maxSetup = 0;

    private void Awake()
    {
        arrows = FindObjectsOfType<Arrow>();
        for (int i = 0; i < arrows.Length; i++)
        {
            if (arrows[i].IDArrow > maxSetup)
                maxSetup = arrows[i].IDArrow;
        }

        if (RandomSetup != 0)
        {
            RandomSetup = (int)Random.Range(1,maxSetup);
            ChoseWhoActive();
        } else
        {
            foreach (var item in arrows)
            {
                item.gameObject.SetActive(false);
            }
        }
    }

    void ChoseWhoActive()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (arrows[i].IDArrow != RandomSetup)
                arrows[i].gameObject.SetActive(false);            
        }
    }
}
