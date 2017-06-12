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
        public float DecayMultiplier = 0f;
        public float RisingMultiplier = 0f;
        public float MinPitchValue;
        public float MaxPitchValue;
        float value;

        public void Init(Ship _ship)
        {
            ship = _ship;
            shipRigid = ship.GetComponent<Rigidbody>();
            AudioSurceAcceleration.clip = GameManager.Instance.AudioMng.ShipAccelerationClip;
            AudioSurceAcceleration.pitch = MinPitchValue;
            value = MinPitchValue;
        }

        private void Update()
        {
            if (Vector3.Angle(shipRigid.velocity, ship.transform.forward) <= 90f && ship.LeftStickDirection != Vector3.zero)
                value += Time.deltaTime * RisingMultiplier;
            else
                value -= Time.deltaTime * DecayMultiplier;

            if (value > MinPitchValue)
            {
                if (!AudioSurceAcceleration.isPlaying)
                    AudioSurceAcceleration.Play();
            }
            //else if(value >= 1) suono a velocità massima
            else
                AudioSurceAcceleration.Stop();

            AudioSurceAcceleration.pitch = value * PitchMultiplier;

            if (AudioSurceAcceleration.pitch > MaxPitchValue)
            {
                AudioSurceAcceleration.pitch = MaxPitchValue;
                value = MaxPitchValue;
            }              

            if (AudioSurceAcceleration.pitch < MinPitchValue)
            {
                AudioSurceAcceleration.pitch = MinPitchValue;
                value = MinPitchValue;
            }
        }
    }
}