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

        public void PurchaseAvatar(AvatarData _data)
        {
            foreach (AvatarData data in LoadAvatarDatas())
            {
                if (data == _data)
                {
                    data.IsPurchased = true;
                    break;
                }
            }
        }

        public void PurchaseColorSet(AvatarData _data, ColorSetData _color)
        {
            foreach (AvatarData data in LoadAvatarDatas())
            {
                if (data == _data)
                {
                    foreach (ColorSetAvailability color in data.ColorSets)
                    {
                        if (color.Color == _color)
                        {
                            color.IsPurchased = true;
                            break;
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Carica l'array di avatar data da Resources
        /// </summary>
        AvatarData[] LoadAvatarDatas()
        {
            return Resources.LoadAll<AvatarData>("ShipModels");            
        }

        /// <summary>
        /// Carica l'array di color set data da Resources
        /// </summary>
        ColorSetData[] LoadColorSets()
        {
            return Resources.LoadAll<ColorSetData>("ShipModels/ColorSets");
        }

        /// <summary>
        /// Istanzia gli avatar data
        /// </summary>
        void InstantiateAvatarDatas()
        {
            foreach (AvatarData data in LoadAvatarDatas())
            {
                if(data.IsPurchased)
                    AvatarDatas.Add(Instantiate(data));
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