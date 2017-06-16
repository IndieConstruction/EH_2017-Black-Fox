using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class ShipAudioSourceController : MonoBehaviour
    {
        public AudioSource AudioSurceAcceleration;
        public AudioSource AudioSurceCollision;
        public AudioSource AudioSurceShoot;
        public AudioSource AudioSourceAmmoRecharge;
        public AudioSource AudioSourceDeath;
        public AudioSource AudioSourceNoAmmo;

        Ship ship;

        [Header("Acceleration Parameters")]
        // Ship Accleleration Parameters
        public float PitchMultiplier = 1f;
        public float DecayMultiplier = 0.085f;
        public float RisingMultiplier = 0.15f;
        public float MinPitchValue = 0f;
        public float MaxPitchValue = 2f;
        float value;

        public void Init(Ship _ship)
        {
            ship = _ship;

            AudioSurceAcceleration.clip = ship.Avatar.AvatarData.ShipAudioSet.Movement;
            AudioSurceCollision.clip = ship.Avatar.AvatarData.ShipAudioSet.Collision;
            AudioSurceShoot.clip = ship.Avatar.AvatarData.ShipAudioSet.Shot;
            AudioSourceAmmoRecharge.clip = ship.Avatar.AvatarData.ShipAudioSet.PinPlaced;
            AudioSourceDeath.clip = ship.Avatar.AvatarData.ShipAudioSet.Death;
            AudioSourceNoAmmo.clip = ship.Avatar.AvatarData.ShipAudioSet.NoAmmo;

            AudioSurceAcceleration.pitch = MinPitchValue;
            value = MinPitchValue;
        }

        #region Play Audios
        public void PlayShootAudio()
        {
            if (AudioSurceShoot.clip != null)
                AudioSurceShoot.Play();
        }

        public void PlayAmmoRechargeAudio()
        {
            if (AudioSourceAmmoRecharge.clip != null)
                AudioSourceAmmoRecharge.Play();
        }

        public void PlayDeathAudio()
        {
            if (AudioSourceDeath.clip != null)
                AudioSourceDeath.Play();
        }

        public void PlayNoAmmoAudio()
        {
            if (AudioSourceNoAmmo.clip != null)
                AudioSourceNoAmmo.Play();
        }

        public void PlayCollisionAudio()
        {
            if (AudioSurceCollision.clip != null)
                AudioSurceCollision.Play();
        }
        #endregion

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