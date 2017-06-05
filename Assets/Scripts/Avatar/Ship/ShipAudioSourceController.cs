using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class ShipAudioSourceController : MonoBehaviour
    {
        public AudioSource AudioSurceAcceleration;
        public AudioSource AudioSurceCollision;
        public AudioSource AudioSurceGeneric;

        Ship ship;
        Rigidbody shipRigid;

        // Ship Accleleration Parameters
        public float PitchMultiplier = 0f;
        float value;

        public void Init(Ship _ship)
        {
            ship = _ship;
            shipRigid = ship.GetComponent<Rigidbody>();
            AudioSurceAcceleration.clip = GameManager.Instance.AudioMng.ShipAccelerationClip;
        }

        private void Update()
        {
            if (Vector3.Angle(shipRigid.velocity, ship.transform.forward) <= 90f)
                value = shipRigid.velocity.magnitude;
            else
                value -= Time.deltaTime;

            if (value > 0)
                AudioSurceAcceleration.Play();
            //else if(value >= 1) suono a velocità massima
            else
                AudioSurceAcceleration.Stop();

            AudioSurceAcceleration.pitch = value * PitchMultiplier;
        }
    }
}