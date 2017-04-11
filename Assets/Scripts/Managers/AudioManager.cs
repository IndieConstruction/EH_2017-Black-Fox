using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class AudioManager : MonoBehaviour
    {
        public AudioClip MenuMovementAudioClip;
        public AudioClip MenuSelectionAudioClip;
        public AudioSource MenuAudioSurce;

        public void PlayMenuMovmentAudio()
        {
            MenuAudioSurce.Stop();
            MenuAudioSurce.clip = MenuMovementAudioClip;
            MenuAudioSurce.Play();
        }

        public void PlayMenuSelectionAudio()
        {
            MenuAudioSurce.Stop();
            MenuAudioSurce.clip = MenuSelectionAudioClip;
            MenuAudioSurce.Play();
        }
    }
}