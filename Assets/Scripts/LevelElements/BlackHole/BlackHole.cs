using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class BlackHole : MonoBehaviour
    {

        public float RangeToDestroyAgents;
        public float RangeToDestroyPins;
        public int Size = 1;
        float Timer = 5;

        // la velocità con cui viene spostato verso il centro
        public float Attraction = 15;

        private void Update()
        {
            ChangeSize(Size);
        }

        void ChangeSize(int _size)
        {
            switch (_size)
            {
                case 1:
                    Attraction = 12;
                    Timer -= Time.deltaTime;
                    if (Timer <= 0)
                    {
                        //Cambiare le dimensioni del Buco Nero
                        Size = 2;
                        Timer = 5;
                    }
                    break;
                case 2:
                    Attraction = 15;
                    Timer -= Time.deltaTime;
                    if (Timer <= 0)
                    {
                        //Cambiare le dimensioni del Buco Nero
                        Size = 3;
                        Timer = 5;
                    }
                    break;
                case 3:
                    Attraction = 18;
                    Timer -= Time.deltaTime;
                    if (Timer <= 0)
                    {
                        Destroy(gameObject);
                    }
                    break;
                default:
                    break;
            }
        }


        private void OnTriggerStay(Collider other)
        {
            GameObject tempObj = other.gameObject;
            
            if (tempObj.tag == "Pin")
            {
                if (Vector3.Distance(tempObj.transform.position, transform.position) <= RangeToDestroyPins)
                {
                    Destroy(tempObj);
                }
            }

            if (tempObj.GetComponent<Agent>() != null)
            {
                tempObj.transform.position = Vector3.MoveTowards(tempObj.transform.position, transform.localPosition, Attraction);
                if (Vector3.Distance(tempObj.transform.position, transform.position) <= RangeToDestroyAgents)
                {
                    Destroy(tempObj);
                }
            }

            if (tempObj.tag == "Core")
            {
                Destroy(gameObject);
            }

            if (tempObj.tag == "BlackHole")
            {
                Destroy(gameObject);
            }
        }
    }
}