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
        public int AddPoints = 1;
        public int SubPoints = 1;
        public int pointsToWin = 5;

        List<PlayerStats> playerStats = new List<PlayerStats>()
        { new PlayerStats(PlayerIndex.One), new PlayerStats(PlayerIndex.Two), new PlayerStats(PlayerIndex.Three), new PlayerStats(PlayerIndex.Four) };

        public void Init(int _killPoints, int _deathPoints, int _pointsToWin)
        {
            AddPoints = _killPoints;
            SubPoints = _deathPoints;
            pointsToWin = _pointsToWin;
        }

        public void UpdateKillPoints(PlayerIndex _killer, PlayerIndex _victim)
        {
            foreach (PlayerStats player in playerStats)
            {
                if (player.PlayerIndex == _killer)
                {
                    player.KillPoints += AddPoints;

                    if (player.KillPoints == pointsToWin)
                    {
                        if (OnPlayerWinnig != null)
                        {
                            player.Victories += 1;
                            if(OnPlayerWinnig != null)
                                OnPlayerWinnig(_killer);
                        }                
                    }
                    break;
                }
            }

            UpdateKillPoints(_victim);
        }

        public void UpdateKillPoints(PlayerIndex _victim)
        {
            foreach (PlayerStats player in playerStats)
            {
                if (player.PlayerIndex == _victim && player.KillPoints > 0)
                {
                    player.KillPoints -= SubPoints;
                    break;
                }
            }
        }

        public void HandleAgentKilled(Agent _killer, Agent _victim)
        {
            if (_killer != null)
                UpdateKillPoints(_killer.playerIndex, _victim.playerIndex);           // setta i punti morte e uccisione
            else
                UpdateKillPoints(_victim.playerIndex);
        }

        #region Events
        public delegate void PointsEvent(PlayerIndex _winner);
        public static PointsEvent OnPlayerWinnig;

        private void OnEnable()
        {
            Agent.AgentKilled += HandleAgentKilled;
        }

        private void OnDisable()
        {
            Agent.AgentKilled -= HandleAgentKilled;
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

    }
}
