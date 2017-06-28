﻿using System.Collections;
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
        public AudioSource AudioSurceGame;
        public AudioSource AudioSurcePowerUp;
        public AudioSource AudioSurceCoin;

        public MenuAudioData MenuAudio;
        public GameAudioData GameAudio;
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
                    if(AudioSurceMenu.clip != null)
                        AudioSurceMenu.Play();
                    break;
                case UIAudio.Selection:
                    AudioSurceMenu.clip = MenuAudio.MenuSelectionAudio.Clip;
                    AudioSurceMenu.volume = MenuAudio.MenuSelectionAudio.Volume;
                    if (AudioSurceMenu.clip != null)
                        AudioSurceMenu.Play();
                    break;
                case UIAudio.Back:
                    AudioSurceMenu.clip = MenuAudio.MenuGoBackAudio.Clip;
                    AudioSurceMenu.volume = MenuAudio.MenuGoBackAudio.Volume;
                    if (AudioSurceMenu.clip != null)
                        AudioSurceMenu.Play();
                    break;
            }
        }

        void PlayGameAudio(AudioInGame _gameAudio)
        {
            switch (_gameAudio)
            {
                case AudioInGame.Count1DownAudio:
                    AudioSurceGame.clip = GameAudio.Count1DownAudio.Clip;
                    AudioSurceGame.volume = GameAudio.Count1DownAudio.Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
                case AudioInGame.Count2DownAudio:
                    AudioSurceGame.clip = GameAudio.Count2DownAudio.Clip;
                    AudioSurceGame.volume = GameAudio.Count2DownAudio.Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
                case AudioInGame.Count3DownAudio:
                    AudioSurceGame.clip = GameAudio.Count3DownAudio.Clip;
                    AudioSurceGame.volume = GameAudio.Count3DownAudio.Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
                case AudioInGame.Round1Audio:
                    AudioSurceGame.clip = GameAudio.Round1Audio.Clip;
                    AudioSurceGame.volume = GameAudio.Round1Audio.Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
                case AudioInGame.Round2Audio:
                    AudioSurceGame.clip = GameAudio.Round2Audio.Clip;
                    AudioSurceGame.volume = GameAudio.Round2Audio.Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
                case AudioInGame.Round3Audio:
                    AudioSurceGame.clip = GameAudio.Round3Audio.Clip;
                    AudioSurceGame.volume = GameAudio.Round3Audio.Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
                case AudioInGame.Round4Audio:
                    AudioSurceGame.clip = GameAudio.Round4Audio.Clip;
                    AudioSurceGame.volume = GameAudio.Round4Audio.Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
                case AudioInGame.Round5Audio:
                    AudioSurceGame.clip = GameAudio.Round5Audio.Clip;
                    AudioSurceGame.volume = GameAudio.Round5Audio.Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
                case AudioInGame.CoinCollected:
                    int random = Random.Range(0, GameAudio.CoinCollected.Count);
                    AudioSurceGame.clip = GameAudio.CoinCollected[random].Clip;
                    AudioSurceGame.volume = GameAudio.CoinCollected[random].Volume;
                    if (AudioSurceGame.clip != null)
                        AudioSurceGame.Play();
                    break;
            }
        }

        void PlayPowerUpSound(PowerUpID _id)
        {
            switch(_id)
            {
                case PowerUpID.Kamikaze:
                    break;
                case PowerUpID.AmmoCleaner:
                    break;
                case PowerUpID.CleanSweep:
                    break;
                case PowerUpID.Tank:
                    break;
                case PowerUpID.InvertCommands:
                    break;
                default:
                    break;
            }
        }

        void PlayMusic(Music _music, bool _play)
        {
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
                                AudioSurceMusic.Play();
                                AudioSurceMusic.DOFade(MenuAudio.MenuMusic.Volume, MusicFadeInTime);
                                break;
                            case Music.GameTheme:
                                AudioSurceMusic.clip = GameAudio.GameplayMusic.Clip;
                                AudioSurceMusic.Play();
                                AudioSurceMusic.DOFade(GameAudio.GameplayMusic.Volume, MusicFadeInTime);
                                break;
                        }
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
            EventManager.OnGameAction += PlayGameAudio;
            EventManager.OnPowerUpAction += PlayPowerUpSound;
        }

        private void OnDisable()
        {
            EventManager.OnMenuAction -= PlayUIAudio;
            EventManager.OnMusicChange -= PlayMusic;
            EventManager.OnGameAction -= PlayGameAudio;
            EventManager.OnPowerUpAction -= PlayPowerUpSound;
        }
        #endregion

        #region Enums
        public enum UIAudio
        {
            Movement,
            Selection,
            Back,
        }

        public enum AudioInGame
        {
            Count1DownAudio,
            Count2DownAudio,
            Count3DownAudio,
            Round1Audio,
            Round2Audio,
            Round3Audio,
            Round4Audio,
            Round5Audio,
            CoinCollected,
            
    }

        public enum Music
        {
            MenuTheme,
            GameTheme,
        }
        #endregion
    }

    [System.Serializable]
    public struct AudioParameter
    {
        public AudioClip Clip;
        [Range(0f, 1f)] public float Volume;
    }
}