using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerTester : MonoBehaviour {

    PoolManager poolManager;
    public GameObject ObjectToInstantiate;


	// Use this for initialization
	void Start () {
        IPoollableObject prefab = ObjectToInstantiate.GetComponent<IPoollableObject>();
        poolManager = new PoolManager(transform, prefab, 10);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            IPoollableObject obj = poolManager.GetPooledObject() ;
            obj.GameObject.transform.position = new Vector3(Random.Range(-3, 3), Random.Range(-3, 3), Random.Range(-3, 3));
        }	
	}


}
