using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BlackFox {

    public class Turrent : MonoBehaviour,IDamageable,IShooter {

        private Transform target;

        public Transform spawner;
        //public Agent player1, player2, player3, player4;
        Agent[] Agents;

        public Rigidbody Bullet;
        public float maxDistance = 100.000f;
        public float Force;
        public float MaxLife = 5;
        public float Life;
        bool IsInactive;
        public float TimeShoot;
        float Timer;
        float TimerToInactivate = 5;
       


        List<IDamageable> damageables = new List<IDamageable>();                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable


        // Use this for initialization
        void Start() {
            IsInactive = false;
            target = null;
            Timer = TimeShoot;
            Life = MaxLife;
            FindPlayersTransfo();
            LoadIDamageablePrefab();
            
        }


        // Update is called once per frame
        void Update() {
            if (IsInactive == false)
            {
                if (target == null)
                {
                    ChooseTarget(Agents[0].GetComponent<Transform>());
                    ChooseTarget(Agents[1].GetComponent<Transform>());
                    ChooseTarget(Agents[2].GetComponent<Transform>());
                    ChooseTarget(Agents[3].GetComponent<Transform>());
                }

                else
                {
                    FollowTarget();
                }
            } else
            {
                TimerToInactivate -= Time.deltaTime;
                if (TimerToInactivate <= 0)
                {
                    IsInactive = false;
                    TimerToInactivate = 5;
                    Life = MaxLife;
                }
                //Rimani fermo.
            }
            
        }


        void FindPlayersTransfo()
        {
            Agents = FindObjectsOfType<Agent>();
        }

        void ChooseTarget(Transform _player)
        {
            if (Vector3.Distance(transform.position, _player.position) < maxDistance && target == null)
                target = _player;

        }


        private void LoadIDamageablePrefab()
        {
            //WARNING - se l'oggetto che che fa parte della lista di GameObject non ha l'interfaccia IDamageable non farà parte degli oggetti danneggiabili.

            List<GameObject> DamageablesPrefabs = PrefabUtily.LoadAllPrefabsWithComponentOfType<IDamageable>("Prefabs", gameObject);

            foreach (var k in DamageablesPrefabs)
            {
                if (k.GetComponent<IDamageable>() != null)
                    damageables.Add(k.GetComponent<IDamageable>());
            }
        }

        void FollowTarget()
        {
            if (Vector3.Distance(target.position, transform.position) < maxDistance)
            {
                Vector3 _target = new Vector3(target.position.x, 0f, target.position.z);
                transform.LookAt(_target);
                Timer -= Time.deltaTime;
                if (Timer <= 0f)
                {
                    GetComponent<ShooterBase>().ShootBullet();
                    
                    Timer = TimeShoot;
                }

            }
            else
            {
                target = null;

            }


        }

        public void Damage(float _damage, GameObject _attacker)
        {
            Life -= _damage;
            if (Life < 1)
            {
                IsInactive = true;
            }
        }

        public List<IDamageable> GetDamageable()
        {
            return damageables;
        }

        public GameObject GetOwner()
        {
            return gameObject;
        }





    }





}