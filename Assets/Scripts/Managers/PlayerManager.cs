using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class PlayerManager : MonoBehaviour
    {
        public List<Player> Players = new List<Player>();

        private void Update()
        {
            UpdatePlayers();
        }

        void UpdatePlayers()
        {
            foreach (Player player in Players)
            {
                player.OnUpdate();
            }
        }
        
        #region API
        /// <summary>
        /// Instanzia i player
        /// </summary>
        public void InstantiatePlayers()
        {
            for(int i = 0; i < 4; i++)
                Players.Add(new Player((PlayerIndex)i));
        }

        /// <summary>
        /// Cambia lo stato del player specificato
        /// </summary>
        /// <param name="_playerState"></param>
        /// <param name="_playerIndex"></param>
        public void ChangePlayerState(PlayerState _playerState, PlayerIndex _playerIndex)
        {
            foreach (Player player in Players)
            {
                if (player.playerIndex == _playerIndex)
                    player.PlayerCurrentState = _playerState;
            }
        }

        /// <summary>
        /// Cambia lo stato di tutti i player
        /// </summary>
        /// <param name="_playerState"></param>
        public void ChangeAllPlayersState(PlayerState _playerState)
        {
            foreach (Player player in Players)
            {
                player.PlayerCurrentState = _playerState;
            }
        }

        /// <summary>
        /// Ritorna il riferimento del player corrispondente all'indice passato
        /// </summary>
        /// <param name="_playerIndex"></param>
        /// <returns></returns>
        public Player GetPlayer(PlayerIndex _playerIndex)
        {
            foreach (Player player in Players)
            {
                if (player.playerIndex == _playerIndex)
                    return player;
            }
            return null;
        }
        #endregion
    }

    /// <summary>
    /// Stati in cui il player può essere
    /// </summary>
    public enum PlayerState
    {
        Blocked,
        MenuInputState,
        PlayInputState
    }
}

