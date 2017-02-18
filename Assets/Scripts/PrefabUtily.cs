using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabUtily {

    public PrefabUtily(){ }

    /// <summary> 
    /// Load ALL resources from the specific path inside the Resources Folder
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_path"></param>
    /// <returns></returns>
    public static List<GameObject> LoadAllPrefabsOfType<T>(string _path)
    {
        List<GameObject> listGameObj = new List<GameObject>();
        GameObject[] prefabs = Resources.LoadAll<GameObject>(_path);
        foreach (var item in prefabs)
        {
            if (item.GetComponent<T>() != null)
            {
                listGameObj.Add(item);
            }
        }
        return listGameObj;
    }

    public static void RemoveObjectFromList(List<GameObject> _list, GameObject _itemToRemove)
    {

        _list.Remove(_itemToRemove);
        
    }
}
