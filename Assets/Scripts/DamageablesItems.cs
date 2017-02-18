using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// #################################################################### DA NON CANCELLARE ! ###############################################################################
public class DamageablesItems {

    List<GameObject> IDamageableGameObj = new List<GameObject>();

    public DamageablesItems()
    {

    }

    public void LoadPrefab()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs/Agents");
        foreach (var item in prefabs)
        {
            if (item.GetComponent<IDamageable>() != null)
            {

            }
        }
        
    }
}
