using System.Collections;
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

        [Header("Audio Clips")]
        public AudioClip MenuMovementAudioClip;
        public AudioClip MenuSelectionAudioClip;
        public AudioClip GameplayAudioClip;
        public AudioClip MenuAudioClip;


        [Header("PowerUp Audio Clips")]
        public AudioClip PowerUpSpawn;
        public AudioClip KamikazeActivation;
        public AudioClip AmmoCleanerActivation;
        public AudioClip CleanSweepActivation;   
        public AudioClip TankActivation;
        public AudioClip InvertCommandsActivation;

        Tweener fade;

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
                                AudioSurceMusic.clip = MenuAudioClip;
                                break;
                            case Music.GameTheme:
                                AudioSurceMusic.clip = GameplayAudioClip;
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
                    clip = KamikazeActivation;
                    break;
                case PowerUpID.AmmoCleaner:
                    clip = AmmoCleanerActivation;
                    break;
                case PowerUpID.CleanSweep:
                    clip = CleanSweepActivation;
                    break;
                case PowerUpID.Tank:
                    clip = TankActivation;
                    break;
                case PowerUpID.InvertCommands:
                    clip = InvertCommandsActivation;
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
}