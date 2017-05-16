using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace BlackFox
{
    public class ShowRoom : MonoBehaviour
    {
        List<GameObject> avatars;
        Vector3 corridorVector;
        int indexOfCurrent;
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
            EvaluateDirection();
            InstanceModels(_data);
        }
        /// <summary>
        /// Dislay next Model
        /// </summary>
        public void ShowNext()
        {
            if(indexOfCurrent < avatars.Count-1)
                modelContainer.transform.DOMove(- corridorVector, 0.5f);
            indexOfCurrent++;
        }
        /// <summary>
        /// Display previous Model
        /// </summary>
        public void ShowPrevious()
        {
            if (indexOfCurrent > 0)
                modelContainer.transform.DOMove(corridorVector, 0.5f);
            indexOfCurrent--;
        }

        #endregion
        /// <summary>
        /// Used to evaluate the positive direction of the ShowRoom
        /// It also istance prevModel
        /// </summary>
        void EvaluateDirection()
        {
            corridorVector = nextModel.position - currentModel.position;

            if (prevModel != null)
                DestroyImmediate(prevModel.gameObject);
          
            prevModel = new GameObject("prevModelPosition").transform;
            prevModel.position = -corridorVector;
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

            for (int i = 0; i < avatars.Count-1; i++)
            {
                avatars.Add(Instantiate(_data[i].ModelPrefab, currentModel.position + corridorVector*i, nextModel.rotation, modelContainer.transform));
            }
            currentModel = avatars[0].transform;
            nextModel = avatars[1].transform;
        }
    }
}
