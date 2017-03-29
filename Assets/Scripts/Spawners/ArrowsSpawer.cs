using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    /// <summary>
    /// Spawn a random Setup between 1 and the higher IDSetup value
    /// </summary>
    public class ArrowsSpawer : SpawnerBase
    {
        Arrow[] arrows;
        int maxRandSetup = 0;
        int randomSetup = 1;

        void Start()
        {            
            if (arrows.Length == 0)
            {
                //Get reference first time
                arrows = FindObjectsOfType<Arrow>();
                //Find the higher IDSetup value
                foreach (Arrow arr in arrows)
                    if (maxRandSetup < arr.IDSetup)
                        maxRandSetup = arr.IDSetup;
            }
            //Pick a random Setup
            randomSetup = Random.Range(1, maxRandSetup);
            //Deactivate all the inactive Setups
            foreach (Arrow arr in arrows)
                if (arr.IDSetup != randomSetup)
                    arr.gameObject.SetActive(false);
        }

        void OnDisable()
        {   
            //Reactivate the deactivated Setups
            foreach (Arrow arr in arrows)
                if (arr.IDSetup != randomSetup)
                    arr.gameObject.SetActive(true);
        }
    }
}
