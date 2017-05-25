using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PoolManager {

    public IPoollableObject ObjectPrefab;
    public int MaxObjectInstanciated = 10;

    List<IPoollableObject> Pool = new List<IPoollableObject>();
    Transform container;

    public PoolManager(Transform _container, IPoollableObject objectToInstantiate, int maxObjectInstanciated) {
        container = _container;
        ObjectPrefab = objectToInstantiate;
        MaxObjectInstanciated = maxObjectInstanciated;
        for (int i = 0; i < MaxObjectInstanciated; i++) {
            createNewObject();
        }
    }

    void createNewObject() {
        IPoollableObject newObject = GameObject.Instantiate(ObjectPrefab.GameObject, container).GetComponent<IPoollableObject>();
        newObject.poolManager = this;
        Pool.Add(newObject);
    }

    #region API

    public IPoollableObject GetPooledObject() {
        if (Pool.FindAll(o => o.IsActive == false).Count < 1) {
            createNewObject();
        }

        
        for (int i = 0; i < Pool.Count; i++) {
            if (Pool[i].IsActive == false) {
                Pool[i].Activate();
                return Pool[i];
            }
        }
        return null;
    }

    public void ReleasedPooledObject(IPoollableObject objectToRelease) {
        for (int i = 0; i < Pool.Count; i++) {
            if (!Pool[i].IsActive) {
                if (objectToRelease.Equals(Pool[i])) {
                    //Pool[i].Deactivate();
                    break;
                }
            }
        }
    }

    #endregion


}
