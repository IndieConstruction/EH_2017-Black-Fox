using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{

    public class OstacoloMobile : MonoBehaviour
    {
        Rigidbody rigid;
        public float InitialImpulse = 1;
        public float SpeedRotation = 2;
        public float damage = 1;
        List<IDamageable> damageables = new List<IDamageable>();                        // Lista di Oggetti facenti parte dell'interfaccia IDamageable
        Core core;


        // Use this for initialization
        void Start()
        {
            core = FindObjectOfType<Core>();
            rigid = GetComponent<Rigidbody>();
            rigid.AddForce(transform.forward * InitialImpulse, ForceMode.Acceleration);
        }

        private void Update()
        {
            rigid.AddForce(transform.forward * InitialImpulse, ForceMode.Acceleration);
        }




        private void OnCollisionEnter(Collision collision)
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            if (Vector3.Cross(transform.position, collision.contacts[0].normal) == Vector3.zero)
            {
                rigid.AddForce(-transform.position * rigid.mass, ForceMode.Impulse);
            }
            else
            {
                rigid.AddForce(Vector3.Reflect(transform.position, collision.contacts[0].normal) * SpeedRotation, ForceMode.Acceleration);
            }
            transform.rotation = Quaternion.LookRotation(rigid.velocity);
            if (damageable != null && collision.gameObject.GetComponent<Core>() == null)
            {
                damageable.Damage(damage, gameObject);
            }
        }
    }
}