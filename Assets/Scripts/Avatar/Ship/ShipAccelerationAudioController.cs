using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class ShipAccelerationAudioController : MonoBehaviour
    {
        float value = 0f;
        public float PitchMultiplier = 2f;
        public AudioSource ShipAudioSurces;
        Ship ship;

        public AudioClip[] ShipAccelerationClip;
        public int Index;

        public void Init(Ship _ship)
        {
            ship = _ship;
            ShipAudioSurces.clip = ShipAccelerationClip[Index];
        }

        private void Update()
        {
            if (value <= 0.1f)
                ShipAudioSurces.Stop();
            //if (value > 0.5)
            value = Mathf.Exp(0.5f * value * value) / Mathf.Sqrt(2 * Mathf.PI);
            //else
            //    value = Mathf.Exp(-0.5f * value * value) / Mathf.Sqrt(2 * Mathf.PI);

            ShipAudioSurces.pitch = value * PitchMultiplier;
        }

        public void Accelerate(float _value)
        {
            if(!ShipAudioSurces.isPlaying)
                ShipAudioSurces.Play();
            value = _value;
        }
    }
}