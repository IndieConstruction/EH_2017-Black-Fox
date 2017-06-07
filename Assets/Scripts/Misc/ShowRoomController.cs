using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlackFox
{
    public class ShowRoomController : MonoBehaviour
    {
        List<GameObject> avatars = new List<GameObject>();
        List<AvatarData> datas { get { return manager.datas; } }
        public Player player;
        SRManager manager;

        Vector3 _corridorVector;
        public Vector3 CorridorVector
        {
            get
            {
                if (_corridorVector == Vector3.zero)
                    EvaluateDirection();
                return _corridorVector;
            }
            protected set
            {
                _corridorVector = value;
            }
        }

        int _indexOfCurrent;
        public int IndexOfCurrent {
            get { return _indexOfCurrent; }
            private set {
                _indexOfCurrent = value;
                if(EventManager.OnShowRoomValueUpdate != null)
                    EventManager.OnShowRoomValueUpdate(manager.datas[0].SelectionParameters[IndexOfCurrent], player);
            }
        }
        public int colorIndex { get; private set; }
        public Transform currentModel;
        public Transform nextModel;
        Transform prevModel;
        GameObject modelContainer;

        #region API
        /// <summary>
        /// Reorder the direction to follow and the Istance of the Avatar Models
        /// </summary>
        /// <param name="_data"></param>
        public void Init(Player myPlayer, SRManager _manager)
        {
            player = myPlayer;
            manager = _manager;
            InstanceModels(datas.ToArray());
            
        }

        /// <summary>
        /// Richiamata nello stato di avatar selection perchè lo show Room viene creato nei menu, ma ancora non sono presenti le slider, di conseguenza la prima volta le slider non hanno valore
        /// </summary>
        public void SetSliderValue()
        {
            if (EventManager.OnShowRoomValueUpdate != null)
                EventManager.OnShowRoomValueUpdate(manager.datas[0].SelectionParameters[IndexOfCurrent], player);
        }

        /// <summary>
        /// Dislay next Model
        /// </summary>
        public void ShowNext()
        {
            if (IndexOfCurrent < avatars.Count - 1)
            {
                IndexOfCurrent++;
                modelContainer.transform.DOMove(-CorridorVector * IndexOfCurrent, 0.5f);
            }
        }
        /// <summary>
        /// Display previous Model
        /// </summary>
        public void ShowPrevious()
        {
            if (IndexOfCurrent > 0)
            {
                IndexOfCurrent--;
                modelContainer.transform.DOMove(-CorridorVector * IndexOfCurrent, 0.5f);
            }
        }

        /// <summary>
        /// Show next color of the ColorSet list of the current AvatarData
        /// </summary>
        public void ShowNextColor()
        {
            colorIndex = manager.GetNextColorID(SRManager.ColorSelectDirection.up, colorIndex);
            foreach (GameObject avatar in avatars)
            {
                foreach (MeshRenderer renderer in avatar.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.materials = new Material[] { datas[IndexOfCurrent].ColorSets[colorIndex].ShipMaterialMain };
                }
            }
        }

        /// <summary>
        /// Show previous color of the ColorSet list of the current AvatarData
        /// </summary>
        public void ShowPreviousColor()
        {
            colorIndex = manager.GetNextColorID(SRManager.ColorSelectDirection.down, colorIndex);
            foreach (GameObject avatar in avatars)
            {
                foreach (MeshRenderer renderer in avatar.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.materials = new Material[] { datas[IndexOfCurrent].ColorSets[colorIndex].ShipMaterialMain };
                }
            }
        }
        #endregion

        /// <summary>
        /// Used to evaluate the positive direction of the ShowRoom
        /// It also istance prevModel
        /// </summary>
        void EvaluateDirection()
        {
            CorridorVector = nextModel.position - currentModel.position;

            if (prevModel != null)
                DestroyImmediate(prevModel.gameObject);

            prevModel = new GameObject("prevModelPosition").transform;
            prevModel.transform.parent = transform;
            prevModel.position = -CorridorVector;
            prevModel.rotation = nextModel.rotation;
        }
        /// <summary>
        /// Place all the required Models in scene along the corridor of the ShowRoom
        /// </summary>
        /// <param name="_data">AvatarDatas of the models</param>
        void InstanceModels(AvatarData[] _data)
        {
            if (modelContainer)
                DestroyImmediate(modelContainer);
            modelContainer = new GameObject("ModelContainer");
            modelContainer.transform.parent = transform;

            for (int i = 0; i < _data.Length; i++)
            {
                avatars.Add(Instantiate(_data[i].ModelPrefab, currentModel.position + CorridorVector * i, nextModel.rotation, modelContainer.transform));
                avatars[i].AddComponent<RotateOnPosition>();
                foreach(MeshRenderer mesh in avatars[i].GetComponentsInChildren<MeshRenderer>())
                {
                    mesh.materials = new Material[] { datas[i].ColorSets[(int)player.ID].ShipMaterialMain };
                }
            }

            currentModel = avatars[0].transform;
            colorIndex = (int)player.ID;
            nextModel = avatars[1].transform;
        }
    }
}
