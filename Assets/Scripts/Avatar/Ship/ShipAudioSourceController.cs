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
        public float PitchMultiplier = 1f;
        public float DecayMultiplier = 1f;
        public float RisingMultiplier = 1f;
        public float MinPitchValue = 0;
        public float MaxPitchValue = 3;
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
            if(ship.LeftStickDirection != Vector3.zero)
                value = Mathf.SmoothStep(AudioSurceAcceleration.pitch, MaxPitchValue, RisingMultiplier);
            else
                value = Mathf.SmoothStep(AudioSurceAcceleration.pitch, MinPitchValue, DecayMultiplier);

            if (AudioSurceAcceleration.pitch > MaxPitchValue)
                AudioSurceAcceleration.pitch = MaxPitchValue;

            if (AudioSurceAcceleration.pitch < MinPitchValue)
                AudioSurceAcceleration.pitch = MinPitchValue;

            if(value > 0)
            {
                if(value == MaxPitchValue)
                {
                    //aggiungi la seconda traccia e la metti in play
                }
                else
                {
                    //spegni la seconda traccia (se c'è)
                }
                AudioSurceAcceleration.pitch = value * PitchMultiplier;
                if (!AudioSurceAcceleration.isPlaying)
                    AudioSurceAcceleration.Play();
            }
            else
            {
                AudioSurceAcceleration.Stop();
            }
        }
    }
}