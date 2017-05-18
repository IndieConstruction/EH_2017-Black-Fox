using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox {
    public class SRManager : MonoBehaviour {

        public GameObject ShowroomPrefab;
        public List<ShowRoomController> rooms = new List<ShowRoomController>();
        List<Player> playersSRs;
        List<RenderTexture> renders = new List<RenderTexture>();
        public List<AvatarData> datas { get { return GameManager.Instance.DataMng.AvatarDatas; } }       


        /// <summary>
        /// Instance new Showrooms and Init them
        /// </summary>
        /// <param name="_datas">Avatar Datas to which contains model to show</param>
        /// <param name="_amountOfRooms">Amount of ShowRooms to create</param>
        public void Init(List<Player> _playerSRs)
        {
            playersSRs = _playerSRs;
            renders = Resources.LoadAll<RenderTexture>("Prefabs/ShowRoom").ToList();
            CreateShowRooms(_playerSRs.Count);
            InitAllRooms();
        }

        /// <summary>
        /// Get next possible color index
        /// </summary>
        /// <param name="_direction"></param>
        /// <param name="_currentColor"></param>
        /// <returns></returns>
        public int GetNextColorID(ColorSelectDirection _direction, int _currentColor)
        {
            if (_direction == ColorSelectDirection.up)
            {
                for (int i = _currentColor; i < datas[0].ColorSets.Count; i++)
                {
                    if (CheckForAvaibility(i))
                        return i;
                }
            }
            else
            {
                for (int i = _currentColor; i >= 0; i--)
                {
                    if (CheckForAvaibility(i))
                        return i;
                }
            }
            return _currentColor;
        }

        bool CheckForAvaibility(int _colorIndex)
        {
            foreach (ShowRoomController room in rooms)
            {
                if (_colorIndex == room.colorIndex)
                    return false;
            }
            return true;
        }

        void CreateShowRooms(int _amountOf)
        {
            GameObject tempSR;
            for (int i = 0; i < _amountOf; i++)
            {
                tempSR = Instantiate(ShowroomPrefab, transform);
                tempSR.transform.localPosition = tempSR.transform.localPosition + Vector3.Cross(tempSR.GetComponent<ShowRoomController>().CorridorVector, transform.forward)*i;
                rooms.Add(tempSR.GetComponent<ShowRoomController>());
                tempSR.GetComponentInChildren<Camera>().targetTexture = renders[i];
            }
        }

        void InitAllRooms()
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                rooms[i].Init(playersSRs[i], this);
            }
        }

        public enum ColorSelectDirection{up=0,down =1}
    }
}