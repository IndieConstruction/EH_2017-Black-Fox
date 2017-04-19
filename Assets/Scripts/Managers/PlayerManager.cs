using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class PlayerManager : MonoBehaviour
    {
        public List<Player> Players = new List<Player>();

        #region API
        /// <summary>
        /// Instanzia i player
        /// </summary>
        public void InstantiatePlayers()
        {
            for(int i = 1; i <= 4; i++)
            {
                Player newPlayer = gameObject.AddComponent<Player>();
                newPlayer.Setup((PlayerLabel)i);
                Players.Add(newPlayer);
            }
                
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
                if (player.PlayerIndex == _playerIndex)
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

        public void ChangeAllPlayersStateExceptOne(PlayerState _playerState, PlayerIndex _playerIndex, PlayerState _otherPlayersState)
        {
            foreach (Player player in Players)
            {
                if (player.PlayerIndex == _playerIndex)
                    player.PlayerCurrentState = _playerState;
                else
                    player.PlayerCurrentState = _otherPlayersState;
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
                if (player.PlayerIndex == _playerIndex)
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

