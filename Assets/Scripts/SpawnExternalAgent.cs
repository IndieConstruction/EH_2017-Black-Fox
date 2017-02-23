using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExternalAgent : MonoBehaviour {

    Transform target;
    float startTime;
    float nextTime;
    public float MinTime;
    public float MaxTime;
    public float angularSpeed;
    public float transSpeed;

	void Start ()
    {
        target = FindObjectOfType<Core>().transform;
        nextTime = Random.Range(MinTime, MaxTime);
    }
	

	void Update ()
    {
         if(Time.time >= startTime + nextTime)
        {
            InstantiateExternalAgent();
            nextTime += Random.Range(MinTime, MaxTime);
        }

        GravityAround();
    }

    void GravityAround()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        
        Quaternion current = transform.localRotation;

        transform.localRotation = Quaternion.Slerp(current, rotation, angularSpeed * Time.deltaTime);
        transform.Translate(transSpeed * Time.deltaTime, 0, 0);
    }

    public void InstantiateExternalAgent()
    {
        GameObject instantiateExternalAgent = Instantiate(LoadIDamageablePrefab(), transform.position, transform.rotation);
        instantiateExternalAgent.GetComponent<ExternalAgent>().SetTarget(target);
    }

    /// <summary>
    /// Ritorna il prefab dell'external agent
    /// </summary>
    private GameObject LoadIDamageablePrefab()
    {
        return Resources.Load<GameObject>("Prefabs/ExternalAgents/ExternalAgent");
    }
}
