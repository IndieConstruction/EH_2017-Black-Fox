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

        public void SetupAvatars() {
            foreach (Player player in Players)
                player.AvatarSetup();
        }

        /// <summary>
        /// Cambia lo stato del player specificato
        /// </summary>
        /// <param name="_playerState"></param>
        /// <param name="_playerID"></param>
        public void ChangePlayerState(PlayerState _playerState, PlayerLabel _playerID)
        {
            foreach (Player player in Players)
            {
                if (player.ID == _playerID)
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

        public void ChangeAllPlayersStateExceptOne(PlayerState _playerState, PlayerLabel _playerID, PlayerState _otherPlayersState)
        {
            foreach (Player player in Players)
            {
                if (player.ID == _playerID)
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
        public Player GetPlayer(PlayerLabel _playerIndex)
        {
            foreach (Player player in Players)
            {
                if (player.ID == _playerIndex)
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
        Blocked = 0,
        MenuInput = 1,
        PlayInput = 2
    }
}

