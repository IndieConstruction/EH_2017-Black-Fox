using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BlackFox
{
    public class DataManager : MonoBehaviour
    {
        [HideInInspector]
        public List<AvatarData> AvatarDatasInstances;

        List<AvatarData> avatarDatas;
        List<ColorSetData> colorData;

        public void Init()
        {
            colorData = LoadColorSets();
            avatarDatas = LoadAvatarDatas();
            InstantiateAvatarDatas(avatarDatas);
        }

        public void PurchaseAvatar(int _dataIndex, AvatarData _data)
        {
            avatarDatas[_dataIndex].IsPurchased = true;
            _data.IsPurchased = true;
        }

        public void PurchaseColorSet(ColorSetData _color)
        {
            foreach (ColorSetData color in colorData)
            {
                if (color == _color)
                {
                    color.IsPurchased = true;
                    break;
                }
            }

        }

        /// <summary>
        /// Carica l'array di avatar data da Resources
        /// </summary>
        List<AvatarData> LoadAvatarDatas()
        {
            return Resources.LoadAll<AvatarData>("ShipModels").ToList();
        }

        /// <summary>
        /// Carica l'array di color set data da Resources
        /// </summary>
        List<ColorSetData> LoadColorSets()
        {
            return Resources.LoadAll<ColorSetData>("ShipModels/ColorSets").ToList();
        }

        /// <summary>
        /// Istanzia gli avatar data
        /// </summary>
        void InstantiateAvatarDatas(List<AvatarData> _datas)
        {
            foreach (AvatarData data in _datas)
            {
                AvatarDatasInstances.Add(Instantiate(data));
            }
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