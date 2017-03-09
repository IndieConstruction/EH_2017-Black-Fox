using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
namespace BlackFox
{
    public class Wave : MonoBehaviour
    {

        public float velocity;
        public float force;
        Vector3 initDist;
        Vector3 deadlyDistance;
        List<PlayerIndex> ListPlayer = new List<PlayerIndex>();

        private void Start()
        {
            //Destroy(gameObject, 7f);
            initDist = transform.position - FindObjectOfType<Core>().transform.position;
        }

        void FixedUpdate()
        {
            MoveForward();
        }

        private void Update()
        {
            deadlyDistance = transform.position - FindObjectOfType<Core>().transform.position;
            Debug.Log(initDist + "/" + deadlyDistance);
            if(Vector3.Distance(initDist, deadlyDistance) <=0)
            {
                Destroy(gameObject);
            }
        }
        void MoveForward()
        {
            transform.Translate(Vector3.forward * velocity);
        }

        void Push(Rigidbody _rigid)
        {
            _rigid.AddForce(transform.forward * force);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Agent>() != null && !ListPlayer.Contains(other.GetComponent<Agent>().playerIndex))
            {
                Push(other.GetComponent<Rigidbody>());
                ListPlayer.Add(other.GetComponent<Agent>().playerIndex);
            }
            else if (other.GetComponent<ExternalAgent>() != null)
            {
                Push(other.GetComponent<Rigidbody>());
            }
        }

        
    }
}
