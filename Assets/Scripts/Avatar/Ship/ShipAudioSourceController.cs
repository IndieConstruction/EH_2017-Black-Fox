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

        List<AudioClip> shootSounds;
        List<AudioClip> collisionSounds;

        public void Init(Ship _ship)
        {
            ship = _ship;

            AudioSurceAcceleration.clip = ship.Avatar.AvatarData.ShipAudioSet.Movements[(int)ship.Avatar.Player.ID -1];
            collisionSounds = ship.Avatar.AvatarData.ShipAudioSet.Collisions;
            shootSounds = ship.Avatar.AvatarData.ShipAudioSet.Shoots;
            AudioSourceAmmoRecharge.clip = ship.Avatar.AvatarData.ShipAudioSet.PinPlaced;
            AudioSourceDeath.clip = ship.Avatar.AvatarData.ShipAudioSet.Death;
            AudioSourceNoAmmo.clip = ship.Avatar.AvatarData.ShipAudioSet.NoAmmo;

            AudioSurceAcceleration.pitch = MinPitchValue;
            value = MinPitchValue;
        }

        #region Play Audios
        public void PlayShootAudio()
        {
            if (shootSounds.Count > 0)
                AudioSurceShoot.clip = shootSounds[Random.Range(0, shootSounds.Count)];
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
            if(collisionSounds.Count > 0)
                AudioSurceCollision.clip = collisionSounds[Random.Range(0, collisionSounds.Count)];
            if (AudioSurceCollision.clip != null && !AudioSurceCollision.isPlaying)
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