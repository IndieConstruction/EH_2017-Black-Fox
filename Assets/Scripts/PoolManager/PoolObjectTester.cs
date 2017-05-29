using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObjectTester : MonoBehaviour, IPoollableObject {

    float timeToKill = 0.9f;

    public GameObject GameObject {
        get {
            return gameObject;
        }
    }

    public bool IsActive {
        get;
        set;
    }

    public PoolManager poolManager {get; set;}

    public void Activate() {
        IsActive = true;
    }

    public void Deactivate() {
        IsActive = false;
        poolManager.ReleasedPooledObject(this);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!IsActive)
            return;
        timeToKill -= Time.deltaTime;
        if (timeToKill < 0) {
            Deactivate();
            timeToKill = 0.9f;
        }
	}

    
}

public interface IPoollableObject {
    PoolManager poolManager { get; set; }
    /// <summary>
    /// Indica il gameobject.
    /// </summary>
    GameObject GameObject { get; }
    bool IsActive { get; set; }

    void Activate();
    void Deactivate();

    
}
