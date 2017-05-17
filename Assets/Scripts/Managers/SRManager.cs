using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class SRManager : MonoBehaviour {

        public GameObject ShowroomPrefab;
        public List<ShowRoom> rooms = new List<ShowRoom>();
        List<RenderTexture> renders = new List<RenderTexture>();

        /// <summary>
        /// Instance new Showrooms and Init them
        /// </summary>
        /// <param name="_datas">Avatar Datas to which contains model to show</param>
        /// <param name="_amountOfRooms">Amount of ShowRooms to create</param>
        public void Init(AvatarData[] _datas, int _amountOfRooms = 4)
        {
            renders = Resources.LoadAll<RenderTexture>("Prefabs/ShowRoom").ToList();
            CreateShowRooms(_amountOfRooms);
            InitAllRooms(_datas);
        }

        void CreateShowRooms(int _amountOf)
        {
            GameObject tempSR;
            for (int i = 0; i < _amountOf; i++)
            {
                tempSR = Instantiate(ShowroomPrefab, transform);
                tempSR.transform.localPosition = tempSR.transform.localPosition + Vector3.Cross(tempSR.GetComponent<ShowRoom>().CorridorVector, transform.forward)*i;
                rooms.Add(tempSR.GetComponent<ShowRoom>());
                tempSR.GetComponentInChildren<Camera>().targetTexture = renders[i];
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