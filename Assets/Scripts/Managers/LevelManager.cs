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

        GameplaySM gameplaySM;
        SpawnerManager spawnerMng;

        void Start()
        {
            spawnerMng = GetComponentInChildren<SpawnerManager>();
            StartGameplaySM(); 
        }

        #region API
        public void HandleAgentKilled(Agent _killer, Agent _victim)
        {
            if (_killer != null)
                UpdateKillPoints(_killer.playerIndex, _victim.playerIndex);           // setta i punti morte e uccisione
                //Aggiorna i punti uccisione sulla UI
            else
                UpdateKillPoints(_victim.playerIndex);
            if (OnPointsUpdate != null)
            {
                OnPointsUpdate();
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

        public void OnLevelStart()
        {
            if(spawnerMng.enabled == false)
                spawnerMng.enabled = true;

            Debug.Log(spawnerMng.enabled);
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
                        if (OnPlayerWinnig != null)
                        {
                            player.Victories += 1;
                            OnPlayerVictory();
                        }                
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
            spawnerMng.enabled = false;
            Debug.Log(spawnerMng.enabled);
            ClearKillPoints();
            if (OnPlayerWinnig != null)
                OnPlayerWinnig();
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
        public delegate void LevelEvent();
        public static LevelEvent OnPlayerWinnig;
        public static LevelEvent OnCoreDeath;
        public static LevelEvent OnPointsUpdate;

        private void OnEnable()
        {
            Agent.OnAgentKilled += HandleAgentKilled;
        }

        private void OnDisable()
        {
            Agent.OnAgentKilled -= HandleAgentKilled;
        }
        #endregion

        #region GameplaySM

        void StartGameplaySM()
        {
            gameplaySM = gameObject.AddComponent<GameplaySM>();
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
