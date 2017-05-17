using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class SRManager : MonoBehaviour {

        public GameObject ShowroomPrefab;
        List<ShowRoom> rooms;

        /// <summary>
        /// Instance new Showrooms and Init them
        /// </summary>
        /// <param name="_datas">Avatar Datas to which contains model to show</param>
        /// <param name="_amountOfRooms">Amount of ShowRooms to create</param>
        public void Init(AvatarData[] _datas, int _amountOfRooms = 4)
        {
            CreateShowRooms(_amountOfRooms);
            InitAllRooms(_datas);
        }

        void CreateShowRooms(int _amountOf)
        {
            for (int i = 0; i < _amountOf; i++)
            {
                rooms.Add(Instantiate(ShowroomPrefab, transform).GetComponent<ShowRoom>());
            }
        }

        void InitAllRooms(AvatarData[] _datas)
        {
            foreach (ShowRoom room in rooms)
            {
                room.Init(_datas);
            }
        }
    }
}