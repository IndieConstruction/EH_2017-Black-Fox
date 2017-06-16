using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class AudioManager : MonoBehaviour
    {
        //TODO : trovare modo funzionale di caricare e leggere le varie audio clips

        public AudioSource AudioSurceMenu;
        public AudioSource AudioSurceMusic;
        public AudioSource AudioSurceAmbience;

        [Header("Audio Clips")]
        public AudioClip MenuMovementAudioClip;
        public AudioClip MenuSelectionAudioClip;
        public AudioClip PowerUpActivation;
        public AudioClip ShipAccelerationClip;

        #region Audio Actions
        void PlayUIAudio(UIAudio _menuAudio)
        {
            switch (_menuAudio)
            {
                case UIAudio.Movement:
                    AudioSurceMenu.clip = MenuMovementAudioClip;
                    AudioSurceMenu.Play();
                    break;
                case UIAudio.Selection:
                    AudioSurceMenu.clip = MenuSelectionAudioClip;
                    AudioSurceMenu.Play();
                    break;
                case UIAudio.Wrong:
                    break;
                case UIAudio.Back:
                    break;
                case UIAudio.CountDown:
                    break;
                default:
                    break;
            }
        }

        void PlayMusic(Music _music)
        {
            switch (_music)
            {
                case Music.MainTheme:
                    //AudioSurceMusic.Play();
                    break;
                case Music.GameTheme:
                    //AudioSurceMusic.Play();
                    break;
                case Music.Ambience:
                    //AudioSurceAmbience.Play();
                    break;
                default:
                    break;
            }
        }

        void PlayPowerUpAudio(PowerUpAudio _powerUpAudio)
        {
            switch (_powerUpAudio)
            {
                case PowerUpAudio.Activation:
                    //AudioSurcePowerUp.Play();
                    break;
                case PowerUpAudio.Spawn:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Events
        private void OnEnable()
        {
            EventManager.OnMenuAction += PlayUIAudio;
            EventManager.OnMusicChange += PlayMusic;
            EventManager.OnPowerUpAction += PlayPowerUpAudio;
        }

        private void OnDisable()
        {
            EventManager.OnMenuAction -= PlayUIAudio;
            EventManager.OnMusicChange -= PlayMusic;
            EventManager.OnPowerUpAction -= PlayPowerUpAudio;
        }
        #endregion

        #region Enums
        public enum UIAudio
        {
            Movement,
            Selection,
            Wrong,
            Back,
            CountDown
        }

        public enum Music
        {
            MainTheme,
            GameTheme,
            Ambience
        }

        public enum PowerUpAudio
        {
            Activation,
            Spawn
        }
        #endregion
    }
}