using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExternalAgent : MonoBehaviour {

    Transform target;
    float nextTime;
    public float MinTime = 10;
    public float MaxTime = 20;
    public float AngularSpeed = 5000;
    public float TransSpeed = 5000;

    List<IDamageable> Damageables = new List<IDamageable>();                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable

    void Start ()
    {
        target = FindObjectOfType<Core>().transform;
        nextTime = Random.Range(MinTime, MaxTime);
    }
	

	void Update ()
    {
         if(Time.time >= nextTime)
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

        transform.localRotation = Quaternion.Slerp(current, rotation, AngularSpeed * Time.deltaTime);
        transform.Translate(TransSpeed * Time.deltaTime, 0, 0);
    }

    public void InstantiateExternalAgent()
    {
        GameObject instantiateExternalAgent = Instantiate(LoadExternalAgentPrefab(), transform.position, transform.rotation);
        instantiateExternalAgent.GetComponent<ExternalAgent>().Initialize(target, Damageables);
    }

    /// <summary>
    /// Salva all'interno della lista di oggetti IDamageable, gli oggetti facenti parti della lista DamageablesPrefabs
    /// </summary>
    private void LoadIDamageablePrefab()
    {
        //WARNING - se l'oggetto che che fa parte della lista di GameObject non ha l'interfaccia IDamageable non farà parte degli oggetti danneggiabili.

        List<GameObject> DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", gameObject);

        foreach (var k in DamageablesPrefabs)
        {
            if (k.GetComponent<IDamageable>() != null)
                Damageables.Add(k.GetComponent<IDamageable>());
        }
    }

    /// <summary>
    /// Ritorna il prefab dell'external agent
    /// </summary>
    private GameObject LoadExternalAgentPrefab()
    {
        return Resources.Load<GameObject>("Prefabs/ExternalAgents/ExternalAgent");
    }
}
