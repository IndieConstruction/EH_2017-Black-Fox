using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlackFox
{
    public class ShowRoom : MonoBehaviour
    {
        List<GameObject> avatars = new List<GameObject>();
        AvatarData[] datas;
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
        int indexOfCurrent;
        int colorIndex;
        public Transform currentModel;
        public Transform nextModel;
        Transform prevModel;
        GameObject modelContainer;

        #region API
        /// <summary>
        /// Reorder the direction to follow and the Istance of the Avatar Models
        /// </summary>
        /// <param name="_data"></param>
        public void Init(AvatarData[] _data)
        {
            datas = _data;
            InstanceModels(_data);
        }
        /// <summary>
        /// Dislay next Model
        /// </summary>
        public void ShowNext()
        {
            if (indexOfCurrent < avatars.Count - 1)
            {
                indexOfCurrent++;
                modelContainer.transform.DOMove(-CorridorVector * indexOfCurrent, 0.5f);
            }
        }
        /// <summary>
        /// Display previous Model
        /// </summary>
        public void ShowPrevious()
        {
            if (indexOfCurrent > 0)
            {
                indexOfCurrent--;
                modelContainer.transform.DOMove(-CorridorVector * indexOfCurrent, 0.5f);
            }
        }

        /// <summary>
        /// Show next color of the ColorSet list of the current AvatarData
        /// </summary>
        public void ShowNextColor()
        {
            if (colorIndex < datas[indexOfCurrent].ColorSets.Count - 1)
            {
                colorIndex++;
                foreach (MeshRenderer renderer in avatars[indexOfCurrent].GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.materials[0] = datas[indexOfCurrent].ColorSets[colorIndex].ShipMaterialMain;
                }
            }
        }

        /// <summary>
        /// Show previous color of the ColorSet list of the current AvatarData
        /// </summary>
        public void ShowPreviousColor()
        {
            if (colorIndex > 0)
            {
                colorIndex--;
                foreach (MeshRenderer renderer in avatars[indexOfCurrent].GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.materials[0] = datas[indexOfCurrent].ColorSets[colorIndex].ShipMaterialMain;
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
                avatars.Add(Instantiate(_data[i].ModelPrefab, currentModel.position + CorridorVector * i, nextModel.rotation, modelContainer.transform));

            currentModel = avatars[0].transform;
            nextModel = avatars[1].transform;
        }
    }
}
