using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class DamageableLoader : MonoBehaviour {

        /// <summary>
        /// La lista di tutti i damageable.
        /// </summary>
        List<GameObject> AllDamageablesPrefabs = new List<GameObject>();


        private void Start()
        {
            OnActivation();
        }

        void OnActivation()
        {
            LoadAllIDamageablePrefabs();
        }

        /// <summary>
        /// Carica la lista di tutti i damageable da resources
        /// </summary>
        void LoadAllIDamageablePrefabs()
        {
            List<GameObject> DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs");

            foreach (var k in DamageablesPrefabs)
            {
                if (k.GetComponent<IDamageable>() != null)
                    AllDamageablesPrefabs.Add(k);
            }
        }

        #region API

        /// <summary>
        /// Ritorna la lista di Idamageable senza il gameobject che viene passato come parametro
        /// </summary>
        /// <param name="_itemToIgnore">L'oggetto da eliminare dalla lista</param>
        /// <returns></returns>
        public List<IDamageable> ReturnDamageablePrefabsForAvatar(GameObject _itemToIgnore)
        {
            List<GameObject> tempList = PrefabUtily.RemoveItemFromList(AllDamageablesPrefabs, _itemToIgnore);
            List<IDamageable> damageables = new List<IDamageable>();

            foreach (var item in tempList)
            {
                if (item.GetComponent<IDamageable>() != null)
                    damageables.Add(item.GetComponent<IDamageable>());
            }

            return damageables;
        }

        #endregion

    }
}