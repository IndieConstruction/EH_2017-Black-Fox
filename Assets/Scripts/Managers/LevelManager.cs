using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    /// <summary>
    /// Gestore del Livello
    /// Condizione vittoria, numero round vinti, passare informazioni livello alla propria morte
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        public int roundNumber = 1;
        public int MaxRound = 4;
        public int lelvelNumber;

        public int AddPoints = 1;
        public int SubPoints = 1;
        public int pointsToWin = 5;

        GameplaySM gameplaySM;
        SpawnerManager spawnerMng;

        void Start()
        {
            spawnerMng = GetComponentInChildren<SpawnerManager>();
            StartGameplaySM(); 
        }

        #region Event Handler
        void HandleOnLevelInit()
        {
            spawnerMng.ReInitLevel();
        }

        void HandleOnLevelPlay() { }

        void HandleOnLevelEnd()
        {

        }
        #endregion

        #region API
        /// <summary>
        /// Viene chiamata quando accade un'uccisione.
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        public void HandleAgentKilled(Agent _killer, Agent _victim)
        {
            if (_killer != null) { 
                UpdateKillPoints(_killer.playerIndex, _victim.playerIndex);           // setta i punti morte e uccisione
                //Aggiorna i punti uccisione sulla UI
                _killer.OnKillingSomeone();
            } else
                UpdateKillPoints(_victim.playerIndex);
            if (EventManager.OnPointsUpdate != null)
            {
                EventManager.OnPointsUpdate();
            }
        }

        public int GetPlayerKillPoints(PlayerIndex _playerIndex)
        {
            foreach (PlayerStats player in playerStats)
            {
                if (player.PlayerIndex == _playerIndex)
                {
                    return player.KillPoints;
                }
            }
            return -1;
        }
        #endregion

        #region KillPoint Count

        List<PlayerStats> playerStats = new List<PlayerStats>()
        {   new PlayerStats(PlayerIndex.One),
            new PlayerStats(PlayerIndex.Two),
            new PlayerStats(PlayerIndex.Three),
            new PlayerStats(PlayerIndex.Four) };

        void UpdateKillPoints(PlayerIndex _killer, PlayerIndex _victim)
        {
            foreach (PlayerStats player in playerStats)
            {
                if (player.PlayerIndex == _killer)
                {
                    player.KillPoints += AddPoints;

                    if (player.KillPoints == pointsToWin)
                    {
                        player.Victories += 1;
                        OnPlayerVictory();
                    }
                    break;
                }
            }

            UpdateKillPoints(_victim);
        }

        void UpdateKillPoints(PlayerIndex _victim)
        {
            foreach (PlayerStats player in playerStats)
            {
                if (player.PlayerIndex == _victim && player.KillPoints > 0)
                {
                    player.KillPoints -= SubPoints;
                    Debug.Log(player.PlayerIndex + "/" + player.KillPoints);
                    break;
                }
            }
        }

        void OnPlayerVictory()
        {
            roundNumber++;
            gameplaySM.SetRoundAndLevelNumber(lelvelNumber, roundNumber);
            ClearKillPoints();
            EventManager.OnPlayerWinnig();
        }
        
        void HandleOnCoreDeath()
        {
            ClearKillPoints();
            spawnerMng.ReInitLevel();
        }

        void ClearKillPoints()
        {
            foreach (PlayerStats player in playerStats)
            {
                player.ResetKillPoints();
            }
        }

        #endregion

        #region Events
        private void OnEnable()
        {
            EventManager.OnAgentKilled += HandleAgentKilled;
            EventManager.OnCoreDeath += HandleOnCoreDeath;

            EventManager.OnLevelInit += HandleOnLevelInit;
            EventManager.OnRoundPlay += HandleOnLevelPlay;
            EventManager.OnRoundEnd += HandleOnLevelEnd;
        }

        private void OnDisable()
        {
            EventManager.OnAgentKilled -= HandleAgentKilled;
            EventManager.OnCoreDeath -= HandleOnCoreDeath;

            EventManager.OnLevelInit -= HandleOnLevelInit;
            EventManager.OnRoundPlay -= HandleOnLevelPlay;
            EventManager.OnRoundEnd -= HandleOnLevelEnd;

        }
        #endregion

        #region GameplaySM

        void StartGameplaySM()
        {
            gameplaySM = gameObject.AddComponent<GameplaySM>();
            gameplaySM.MaxRound = this.MaxRound;
            gameplaySM.SetRoundAndLevelNumber(lelvelNumber, roundNumber);
        }

        #endregion
    }

    /// <summary>
    /// Contenitore dei punti del player
    /// </summary>
    public class PlayerStats
    {
        PlayerIndex playerIndex;
        int powerPoints;
        int killPoints;
        int victories;

        public PlayerIndex PlayerIndex
        {
            get { return playerIndex; }
        }

        public int KillPoints
        {
            get { return killPoints; }
            set { killPoints = value; }
        }

        public int PowerPoints
        {
            get { return powerPoints; }
            set { powerPoints = value; }
        }

        public int Victories
        {
            get { return powerPoints; }
            set { powerPoints = value; }
        }

        public PlayerStats(PlayerIndex _playerIndex)
        {
            playerIndex = _playerIndex;
        }

        public void ResetKillPoints()
        {
            killPoints = 0;
        }

    }
}
