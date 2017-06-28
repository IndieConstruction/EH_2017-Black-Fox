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
        public ExternalAgentsAudioData ExternalAgentsAudio;

        Tweener fade;

        #region Audio Actions
        void PlayUIAudio(UIAudio _menuAudio)
        {
            switch (_menuAudio)
            {
                case UIAudio.Movement:
                    AudioSurceMenu.clip = MenuAudio.MenuMovementAudio.Clip;
                    AudioSurceMenu.volume = MenuAudio.MenuMovementAudio.Volume;
                    AudioSurceMenu.Play();
                    break;
                case UIAudio.Selection:
                    AudioSurceMenu.clip = MenuAudio.MenuSelectionAudio.Clip;
                    AudioSurceMenu.volume = MenuAudio.MenuSelectionAudio.Volume;
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
                                AudioSurceMusic.volume = MenuAudio.MenuMusic.Volume;
                                break;
                            case Music.GameTheme:
                                AudioSurceMusic.clip = MenuAudio.GameplayMusic.Clip;
                                AudioSurceMusic.volume = MenuAudio.GameplayMusic.Volume;
                                break;
                        }
                        AudioSurceMusic.Play();
                        AudioSurceMusic.DOFade(1, MusicFadeInTime);
                    }
                });
            }
        }

        public AudioParameter GetPowerUpClip(PowerUpID _powerUpID)
        {
            AudioParameter audioParameter = new AudioParameter();
            switch (_powerUpID)
            {
                case PowerUpID.Kamikaze:
                    audioParameter.Clip = PowerUpAudio.KamikazeActivation.Clip;
                    audioParameter.Volume = PowerUpAudio.KamikazeActivation.Volume;
                    break;
                case PowerUpID.AmmoCleaner:
                    audioParameter.Clip = PowerUpAudio.AmmoCleanerActivation.Clip;
                    audioParameter.Volume = PowerUpAudio.AmmoCleanerActivation.Volume;
                    break;
                case PowerUpID.CleanSweep:
                    audioParameter.Clip = PowerUpAudio.CleanSweepActivation.Clip;
                    audioParameter.Volume = PowerUpAudio.CleanSweepActivation.Volume;
                    break;
                case PowerUpID.Tank:
                    audioParameter.Clip = PowerUpAudio.TankActivation.Clip;
                    audioParameter.Volume = PowerUpAudio.TankActivation.Volume;
                    break;
                case PowerUpID.InvertCommands:
                    audioParameter.Clip = PowerUpAudio.InvertCommandsActivation.Clip;
                    audioParameter.Volume = PowerUpAudio.InvertCommandsActivation.Volume;
                    break;
            }
            return audioParameter;
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