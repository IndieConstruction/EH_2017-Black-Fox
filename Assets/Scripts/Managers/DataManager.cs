using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class DataManager : MonoBehaviour
    {
        List<AvatarData> _avatarDatas = new List<AvatarData>();
        public List<AvatarData> AvatarDatas;

        public void Init()
        {
            InstantiateAvatarDatas();
        }

        /// <summary>
        /// Carica l'array di avatar data da Resources
        /// </summary>
        AvatarData[] LoadAvatarDatas()
        {
            return Resources.LoadAll<AvatarData>("ShipModels");            
        }

        /// <summary>
        /// Istanzia gli avatar data
        /// </summary>
        void InstantiateAvatarDatas()
        {
            foreach (AvatarData data in LoadAvatarDatas())
                AvatarDatas.Add(Instantiate(data));
        }
    }

    public class ConstrainedAvatarData
    {
        public Player player;
        public int SelectedDataIndex;
        public int SelectedDataColorId;

        public ConstrainedAvatarData(Player _player, int _selectedDataColorId = 0, int _selectedDataIndex = 0)
        {
            player = _player;
            SelectedDataIndex = _selectedDataIndex;
            SelectedDataColorId = _selectedDataColorId;
        }
    }
}