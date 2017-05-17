using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class SRManager : MonoBehaviour {

        public GameObject ShowroomPrefab;
        List<ShowRoom> rooms = new List<ShowRoom>();

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
            GameObject tempSR;
            for (int i = 0; i < _amountOf; i++)
            {
                tempSR = Instantiate(ShowroomPrefab, transform);
                rooms.Add(tempSR.GetComponent<ShowRoom>());
                tempSR.transform.localPosition = tempSR.transform.localPosition + Vector3.Cross(tempSR.GetComponent<ShowRoom>().CorridorVector, transform.forward);                
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