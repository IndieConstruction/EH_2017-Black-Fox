using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWave : MonoBehaviour {

    List<Transform> SpawnPoints = new List<Transform>();
    GameObject wave;
    float nextTime;
    public float MinTime = 20;
    public float MaxTime = 50;

    void Start ()
    {
        SetSpawnPoints();
        //nextTime += Random.Range(MinTime, MaxTime);
    }
	

	void Update ()
    {
        if (Time.time >= nextTime)
        {
            int spawn = (int)Random.Range(0f, SpawnPoints.Count);
            InstantiateWave(SpawnPoints[spawn]);
            nextTime += Random.Range(MinTime, MaxTime);
        }
    }
    
    void SetSpawnPoints()
    {
        Transform[] spwans = GetComponentsInChildren<Transform>();

        for (int i = 0; i < spwans.Length; i++)
        {
            if(spwans[i].gameObject != gameObject)
            {
                SpawnPoints.Add(spwans[i]);
            }
        }
    }

    void InstantiateWave(Transform _spawn)
    {
        if (!wave)
            wave = Instantiate(LoadExternalAgentPrefab(), _spawn.position, _spawn.rotation);
        else
        {
            wave.transform.position = _spawn.position;
            wave.transform.rotation = _spawn.rotation;
        }
            
    }

    private GameObject LoadExternalAgentPrefab()
    {
        return Resources.Load<GameObject>("Prefabs/LevelElements/Wave");
    }
}
