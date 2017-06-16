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
        public AudioClip GameplayAudioClip;
        public AudioClip PowerUpActivation;

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
            }
        }

        void PlayMusic(Music _music, bool _play)
        {
            if(_play)
            {
                switch (_music)
                {
                    case Music.MainTheme:
                        //AudioSurceMusic.Play();
                        break;
                    case Music.GameTheme:
                        AudioSurceMusic.clip = GameplayAudioClip;
                        AudioSurceMusic.Play();
                        break;
                    case Music.Ambience:
                        //AudioSurceAmbience.Play();
                        break;
                }
            }
            else
            {
                switch (_music)
                {
                    case Music.MainTheme:
                        //StartCoroutine(FadeoutMusic(AudioSurceMusic, 1.5f));
                        break;
                    case Music.GameTheme:
                        StartCoroutine(FadeoutMusic(AudioSurceMusic, 1.5f));
                        break;
                    case Music.Ambience:
                        //StartCoroutine(FadeoutMusic(AudioSurceAmbience, 1f));
                        break;
                }
            }
        }
        #endregion

        IEnumerator FadeoutMusic(AudioSource _surceToFade, float _speed)
        {
            while (_surceToFade.volume > 0)
            {
                _surceToFade.volume -= _speed * Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            _surceToFade.Stop();
        }

        #region Events
        private void OnEnable()
        {
            EventManager.OnMenuAction += PlayUIAudio;
            EventManager.OnMusicChange += PlayMusic;
        }

        private void OnDisable()
        {
            EventManager.OnMenuAction -= PlayUIAudio;
            EventManager.OnMusicChange -= PlayMusic;
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
        #endregion
    }
}