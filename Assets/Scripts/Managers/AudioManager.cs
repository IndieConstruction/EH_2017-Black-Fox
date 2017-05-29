using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class AudioManager : MonoBehaviour
    {
        //TODO : trovare modo funzionale di acricare e leggere le varie audio clips
        public AudioClip MenuMovementAudioClip;
        public AudioClip MenuSelectionAudioClip;

        public AudioSource AudioSurceMenu;
        public AudioSource AudioSurceMusic;
        public AudioSource AudioSurceAmbience;
        public AudioSource AudioSurcePowerUp;
        public AudioSource[] PlayerAudioSurces;

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

        void PlayAvatarAudio(AvatarAudio _avatarAudio, PlayerLabel _playerId)
        {
            for (int i = 0; i < PlayerAudioSurces.Length; i++)
            {
                if (PlayerAudioSurces[i].name == "AudioSourcePlayer" + (int)_playerId)
                {
                    switch (_avatarAudio)
                    {
                        case AvatarAudio.AmmoRecharge:
                            //PlayerAudioSurces[i].Play();
                            break;
                        case AvatarAudio.Death:
                            break;
                        case AvatarAudio.Movement:
                            break;
                        case AvatarAudio.Shot:
                            break;
                        case AvatarAudio.NoAmmo:
                            break;
                        case AvatarAudio.PinPlaced:
                            break;
                        case AvatarAudio.Collision:
                            break;
                        default:
                            break;
                    }
                }
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
            EventManager.OnAvatarAction += PlayAvatarAudio;
            EventManager.OnPowerUpAction += PlayPowerUpAudio;
        }

        private void OnDisable()
        {
            EventManager.OnMenuAction -= PlayUIAudio;
            EventManager.OnMusicChange -= PlayMusic;
            EventManager.OnAvatarAction -= PlayAvatarAudio;
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

        public enum AvatarAudio
        {
            AmmoRecharge,
            Death,
            Movement,
            Shot,
            NoAmmo,
            PinPlaced,
            Collision
        }

        public enum PowerUpAudio
        {
            Activation,
            Spawn
        }
        #endregion
    }
}