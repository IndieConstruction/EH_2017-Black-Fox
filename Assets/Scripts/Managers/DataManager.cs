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

        List<ModelConstrain> modelConstrains = new List<ModelConstrain>();

        public void Init()
        {
            InstantiateAvatarDatas();
            InitConstrains(GameManager.Instance.PlayerMng.Players);
        }

        public void SetConstrainsAvatar(Player _player, AvatarData _data)
        {
            for (int i = 0; i < modelConstrains.Count; i++)
            {
                if (modelConstrains[i].player == _player)
                    modelConstrains[i].avatarData = AvatarDatas[i];
            }
        }

        public void SetConstrainsColor(Player _player, ColorSetData _color)
        {
            for (int i = 0; i < modelConstrains.Count; i++)
            {
                if (modelConstrains[i].player == _player)
                    modelConstrains[i].colorSet = _color;
            }
        }

        public List<ModelConstrain> GetConstrains(Player _player)
        {
            List<ModelConstrain> constrains = new List<ModelConstrain>();
            for (int i = 0; i < modelConstrains.Count; i++)
            {
                if (modelConstrains[i].player != _player)
                    constrains.Add(modelConstrains[i]);
            }
            return constrains;
        }

        void InitConstrains(List<Player> _playerList)
        {
            for (int i = 0; i < _playerList.Count; i++)
                modelConstrains.Add(new ModelConstrain(_playerList[i]));

            for (int i = 0; i < modelConstrains.Count && i < AvatarDatas.Count; i++)
            {
                if (modelConstrains[i].avatarData == null)
                    modelConstrains[i].avatarData = AvatarDatas[i];

                if (modelConstrains[i].colorSet == null)
                    modelConstrains[i].colorSet = AvatarDatas[i].ColorSets[i];
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
    }

    public class ModelConstrain
    {
        public Player player;
        public AvatarData avatarData;
        public ColorSetData colorSet;

        public ModelConstrain(Player _player)
        {
            player = _player;
        }
    }
}