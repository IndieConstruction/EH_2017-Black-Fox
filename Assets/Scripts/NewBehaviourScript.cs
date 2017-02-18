using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public GameObject bbb;

	// Use this for initialization
	void Start () {
        List<GameObject> aaa = PrefabUtily.LoadAllPrefabsOfType<IDamageable>("Prefabs/Agents");
        PrefabUtily.RemoveObjectFromList(aaa, bbb);
        Debug.Log(aaa.Count);
    }
	
	// Update is called once per frame
	void Update () {
        

    }
}
