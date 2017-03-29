using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    /// <summary>
    /// Tiene riferimenti agli oggeti in scena da distruggere
    /// </summary>
    public static class SceneCleaner
    {
        static List<GameObject> ObjectToDestroy = new List<GameObject>();

        #region API
        /// <summary>
        /// Aggunge elemto da ditruggere
        /// </summary>
        /// <param name="_obj"></param>
        public static void CollectObject(GameObject _obj)
        {
            if(_obj != null)
                ObjectToDestroy.Add(_obj);
        }

        /// <summary>
        /// Toglie un elemto dalla lista di oggetti da distruggere
        /// </summary>
        /// <param name="_obj"></param>
        public static void SaveItem(GameObject _obj)
        {
            if(_obj != null)
                ObjectToDestroy.Remove(_obj);
        }

        /// <summary>
        /// Distrugge tutti gli elementi
        /// </summary>
        public static void Clean()
        {
            foreach (GameObject item in ObjectToDestroy)
            {
                if(item != null)
                    GameObject.Destroy(item);
                ObjectToDestroy.TrimExcess();
            }
        }

        public static void CleanScene()
        {

        }
        #endregion
    }
}