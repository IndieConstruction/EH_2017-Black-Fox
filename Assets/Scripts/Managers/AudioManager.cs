using System.Collections;
using System;
using DG.Tweening;
using UnityEngine;

namespace BlackFox
{
    public class AudioManager : MonoBehaviour
    {
        public float MusicFadeOutTime = 0.8f;
        public float MusicFadeInTime = 0.1f;

        public AudioSource AudioSurceMenu;
        public AudioSource AudioSurceMusic;

        public MenuAudioData MenuAudio;
        public PowerUpAudioData PowerUpAudio;

        Tweener fade;

        #region Audio Actions
        void PlayUIAudio(UIAudio _menuAudio)
        {
            switch (_menuAudio)
            {
                case UIAudio.Movement:
                    AudioSurceMenu.clip = MenuAudio.MenuMovementAudio.Clip;
                    AudioSurceMenu.Play();
                    break;
                case UIAudio.Selection:
                    AudioSurceMenu.clip = MenuAudio.MenuSelectionAudio.Clip;
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
            AudioSource surce = AudioSurceMusic;
            if (AudioSurceMusic != null)
            {
                fade = AudioSurceMusic.DOFade(0, MusicFadeOutTime).OnComplete(() =>
                {
                    if (_play)
                    {
                        switch (_music)
                        {
                            case Music.MenuTheme:
                                AudioSurceMusic.clip = MenuAudio.MenuMusic.Clip;
                                break;
                            case Music.GameTheme:
                                AudioSurceMusic.clip = MenuAudio.GameplayMusic.Clip;
                                break;
                        }
                        AudioSurceMusic.Play();
                        AudioSurceMusic.DOFade(1, MusicFadeInTime);
                    }
                });
            }
        }

        public AudioClip GetPowerUpClip(PowerUpID _powerUpID)
        {
            AudioClip clip = null;
            switch (_powerUpID)
            {
                case PowerUpID.Kamikaze:
                    clip = PowerUpAudio.KamikazeActivation.Clip;
                    break;
                case PowerUpID.AmmoCleaner:
                    clip = PowerUpAudio.AmmoCleanerActivation.Clip;
                    break;
                case PowerUpID.CleanSweep:
                    clip = PowerUpAudio.CleanSweepActivation.Clip;
                    break;
                case PowerUpID.Tank:
                    clip = PowerUpAudio.TankActivation.Clip;
                    break;
                case PowerUpID.InvertCommands:
                    clip = PowerUpAudio.InvertCommandsActivation.Clip;
                    break;
            }
            return clip;
        }
        #endregion

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
            MenuTheme,
            GameTheme,
        }
        #endregion
    }

    [Serializable]
    public struct AudioParameter
    {
        public AudioClip Clip;
        [Range(0f, 1f)] public float Volume;
    }
}