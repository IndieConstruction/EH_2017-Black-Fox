using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class DataManager : MonoBehaviour
    {
        List<AvatarData> _avatarDatas = new List<AvatarData>();
        public List<AvatarData> AvatarDatas
        {
            get { return _avatarDatas; }
            private set { _avatarDatas = value; }
        }

        public void Init()
        {
            InstantiateAvatarDatas();
        }

        /// <summary>
        /// Carica l'array di avatar data da Resources
        /// </summary>
        AvatarData[] LoadAvatarDatas()
        {
            return Resources.LoadAll("ShipModels") as AvatarData[];            
        }

        /// <summary>
        /// Istanzia gli avatar data
        /// </summary>
        void InstantiateAvatarDatas()
        {
            foreach (AvatarData data in LoadAvatarDatas())
                AvatarDatas.Add(Instantiate(data as AvatarData));
        }
    }
}