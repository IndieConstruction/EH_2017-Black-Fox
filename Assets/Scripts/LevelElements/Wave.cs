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
                ListPlayer.Add(other.GetComponent<Agent>().playerIndex);
                Push(other.GetComponent<Rigidbody>());
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
                //Destroy(gameObject);
            }

        }
    }
}
