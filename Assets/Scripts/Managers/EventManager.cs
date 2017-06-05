using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public static class EventManager
    {
        #region AgentSpawn
        public delegate void AgentSpawnEvent(Avatar _agent);
        /// <summary>
        /// Evento chiamato al respawn di un agente
        /// </summary>
        public static AgentSpawnEvent OnAgentSpawn;
        #endregion

        #region AvatarEvents

        public delegate void AvatarUIEvent(Avatar _avatar);

        /// <summary>
        /// Evento che avvisa lo UIManager di aggiornare le munizioni per un dato Avatar
        /// </summary>
        public static AvatarUIEvent OnAmmoValueChange;
        /// <summary>
        /// Evento da chiamare alla variazione delle vita del avatar(o della ship collegata)
        /// </summary>
        public static AvatarUIEvent OnLifeValueChange;

        #region AgentKilledEvent
        public delegate void AgentKilledEvent(Avatar _killer, Avatar _victim);
        /// <summary>
        /// Evento chiamato alla morte di un agente
        /// </summary>
        public static AgentKilledEvent OnAgentKilled;
        #endregion

        #endregion

        #region LevelEvent
        public delegate void LevelEvent();

        /// <summary>
        /// Evento chiamato alla morte del core o alla vittoria dell'agente che innesca il cambio di stato della GameplaySM
        /// </summary>
        //public static LevelEvent TriggerPlayStateEnd;
        #endregion

        #region UIEvent
        public delegate void UIEvent();
        /// <summary>
        /// Evento che si occupa di innescare l'update dei punti sulla UI
        /// </summary>
        public static UIEvent OnPointsUpdate;
        #endregion

        #region Audio Event
        public delegate void UIAudioEvent(AudioManager.UIAudio _menuAudio);
        /// <summary>
        /// Evento che si occupa di innescare i souni dei player nei menù
        /// </summary>
        public static UIAudioEvent OnMenuAction;

        public delegate void MusicEvent(AudioManager.Music _music);
        /// <summary>
        /// Evento che si occupa di innescare la musica
        /// </summary>
        public static MusicEvent OnMusicChange;

        public delegate void PowerUpAudioEvent(AudioManager.PowerUpAudio _powerUpAudio);
        /// <summary>
        /// Evento che si occupa di innescare i souni dei power up in gioco
        /// </summary>
        public static PowerUpAudioEvent OnPowerUpAction;
        #endregion
    }
}