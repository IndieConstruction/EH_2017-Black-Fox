using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class ExternalElementSpawner : SpawnerBase
    {
        public GameObject ExternalAgent;                        //Prefab of the ExternalAgent to instantiate         
        public float MinTime = 10;                              //Min time between Spawns
        public float MaxTime = 20;                              //Max time between Spawns
        public float AngularSpeed = 1;                          //Rotation speed
        public float TransSpeed = 1;                            //Precession speed
        Transform target;                                       //Target of the ExternalElements
        float nextTime;                                         //Timer
        List<IDamageable> Damageables = new List<IDamageable>();//Lista di oggetti danneggiabili

        #region SpawnerLifeFlow
        void Start()
        {
            if(ExternalAgent == null)
                ExternalAgent = (GameObject)Resources.Load("Prefabs/ExternalAgents/ExternalAgent1");

            target = FindObjectOfType<Core>().transform;
            nextTime = Random.Range(MinTime, MaxTime);
            LoadIDamageablePrefab();
        }

        void Update()
        {
            if (Time.time >= nextTime)
            {
                InstantiateExternalAgent();
                nextTime += Random.Range(MinTime, MaxTime);
            }
            GravityAround();
        }
        #endregion

        /// <summary>
        /// Rotate around the target position and keep facing it
        /// </summary>
        void GravityAround()
        {
            Vector3 relativePos = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);

            Quaternion current = transform.localRotation;

            transform.localRotation = Quaternion.Slerp(current, rotation, AngularSpeed * Time.deltaTime);
            transform.Translate(TransSpeed * Time.deltaTime, 0, 0);
        }

        /// <summary>
        /// Load damageable items (classes with IDamageable) from prefabs
        /// </summary>
        void LoadIDamageablePrefab()
        {
            //WARNING - If a GameObject in the list do not have the IDamageable interface, it will not be damaged
            List<GameObject> DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", ExternalAgent);
            foreach (var k in DamageablesPrefabs)
            {
                if (k.GetComponent<IDamageable>() != null)
                    Damageables.Add(k.GetComponent<IDamageable>());
            }
        }

        /// <summary>
        /// Instatiate an External Agent
        /// </summary>
        void InstantiateExternalAgent()
        {
            GameObject instantiateEA = Instantiate(ExternalAgent, transform.position, transform.rotation);
            ExternalAgent eA = instantiateEA.GetComponent<ExternalAgent>();
            eA.Initialize(target, Damageables);
        }
    }
}
