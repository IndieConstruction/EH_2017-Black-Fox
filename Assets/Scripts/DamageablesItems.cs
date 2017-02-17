using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// #################################################################### DA NON CANCELLARE ! ###############################################################################
public class DamageablesItems {

    public DamageablesItems()
    {

    }

    void LoadPrefab()
    {
        GameObject[] prefabs = Resources.LoadAll<GameObject>("/Prefabs/Agents");
        Debug.Log(prefabs.Length);
    }
}
