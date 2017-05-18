using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class DataManager : MonoBehaviour
    {
        List<AvatarData> _avatarDatas = new List<AvatarData>();
        public List<AvatarData> AvatarDatas;
        List<ConstrainedAvatarData> _playerSelection;
        public List<ConstrainedAvatarData> PlayerSelection {
            get { return _playerSelection; }
            set { _playerSelection = value; }
        }

        public void Init()
        {
            InstantiateAvatarDatas();
            EvaluateStartingSelections();
        }


        void EvaluateStartingSelections()
        {
            int firstDataIndex = 0;
            int firstColorID = 0;
            foreach (Player player in GameManager.Instance.PlayerMng.Players)
            {
                PlayerSelection.Add(new ConstrainedAvatarData(player, firstColorID, firstDataIndex));

                if (firstDataIndex < AvatarDatas.Count - 1)
                    firstDataIndex++;
                else
                    firstDataIndex = 0;

                if (firstColorID < AvatarDatas[firstDataIndex - 1].ColorSets.Count - 1)
                    firstColorID++;
                else
                    firstColorID = 0;
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
        /// Istanzia gli avatar data
        /// </summary>
        void InstantiateAvatarDatas()
        {
            foreach (AvatarData data in LoadAvatarDatas())
                AvatarDatas.Add(Instantiate(data));
        }

        ConstrainedAvatarData OnConstrainSet(ConstrainedAvatarData _newData)
        {
            if (_newData.SelectedDataColorId >= _avatarDatas[_newData.SelectedDataIndex].ColorSets.Count)
                return new ConstrainedAvatarData(_newData.player, 0, _newData.SelectedDataIndex);

            if (_newData.SelectedDataIndex >= _avatarDatas.Count)
                return new ConstrainedAvatarData(_newData.player);

            ConstrainedAvatarData _oldData = new ConstrainedAvatarData(_newData.player);
            foreach (ConstrainedAvatarData currData in PlayerSelection)
                if (_newData.player == currData.player)
                    _oldData = _newData;

            foreach (ConstrainedAvatarData selection in PlayerSelection)
            {
                if (selection.SelectedDataColorId == _newData.SelectedDataColorId)
                    return new ConstrainedAvatarData(_newData.player, (_newData.SelectedDataColorId < _oldData.SelectedDataColorId) ? _newData.SelectedDataColorId-- : _newData.SelectedDataColorId++);
            }

            return _newData;
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