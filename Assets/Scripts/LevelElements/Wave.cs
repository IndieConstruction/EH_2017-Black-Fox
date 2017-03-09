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
        List<PlayerIndex> ListPlayer = new List<PlayerIndex>();

        private void Start()
        {
            Destroy(gameObject, 7f);
        }

        void FixedUpdate()
        {
            MoveForward();
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
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Wall")
            {
                Debug.Log("kill");
                //TODO : Destroy(gameObject);
            }

        }
    }
}
