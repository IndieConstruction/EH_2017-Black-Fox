using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BlackFox
{
    public class DataManager : MonoBehaviour
    {
        [HideInInspector]
        public List<AvatarData> AvatarDatasInstances { get { return DataSavedToAvatarData(datas); } }

        List<ColorSetData> colorData;

        List<DataSaved> datas = new List<DataSaved>();

        public void Init()
        {
            colorData = LoadColorSets();
            InstantiateAvatarDatas(LoadAvatarDatas());
        }

        /// <summary>
        /// Setta la variabile in PlayerPref corrispondente al dato che viene passato, sostituisce la struttura contenuta in datas con una nuova con valori aggiornati
        /// </summary>
        /// <param name="_dataIndex"></param>
        /// <param name="_data">L'avatar data che deve essere modificato</param>
        public void PurchaseAvatar(AvatarData _data)
        {
            PlayerPrefs.SetInt(_data.DataName, 1);

            for (int i = 0; i < datas.Count; i++)
            {
                if (_data.DataName == datas[i].avatar.DataName)
                {
                    AvatarData tempData = datas[i].avatar;                                  // Riutilizzo l'avatarData già contenuta all'interno di datas.avatar
                    datas[i] = new DataSaved() { avatar = tempData, isPurchased = 1 };
                }
            }

            //avatarDatas[_dataIndex].IsPurchased = true;
            //_data.IsPurchased = true;
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
            // controlla chi è purchase e chi no
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
                DataSaved tempData = new DataSaved();
                tempData.avatar = Instantiate(data);
                if (PlayerPrefs.HasKey(data.DataName)) 
                    tempData.isPurchased = PlayerPrefs.GetInt(data.DataName);
                else
                    tempData.isPurchased = data.IsPurchased == true ? 1 : 0;

                datas.Add(tempData);
            }
        }

        List<AvatarData> DataSavedToAvatarData(List<DataSaved> _data)
        {
            List<AvatarData> DataToReturn = new List<AvatarData>();
            foreach (DataSaved data in _data)
            {
                DataToReturn.Add(data.avatar);
            }
            return DataToReturn;
        }

        /// <summary>
        /// Resetta le monete ed i modelli
        /// </summary>
        public void DataReset()
        {
            ResetModelsPurchased();
        }

        /// <summary>
        /// Resetta tutti i modelli a Purchase tranne il gufo
        /// </summary>
        void ResetModelsPurchased()
        {
            for (int i = 0; i < datas.Count; i++)
            {
                if (datas[i].avatar.DataName != "Owl")
                {
                    PlayerPrefs.SetInt(datas[i].avatar.DataName, 0);
                    datas[i].avatar.IsPurchased = false;
                }
                else
                {
                    PlayerPrefs.SetInt(datas[i].avatar.DataName, 1);
                    datas[i].avatar.IsPurchased = true;
                }
            }
        }

        struct DataSaved
        {
            public AvatarData avatar;
            public int isPurchased { set { avatar.IsPurchased = value == 0 ?  false : true; } }  // 1: Purchased, 0: non Purchased
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