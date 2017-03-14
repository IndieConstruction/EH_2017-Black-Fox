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
        int AddPoints = 1;
        int SubPoints = 1;
        int pointsToWin = 5;

        List<PlayerPoints> pointsManager = new List<PlayerPoints>()
        { new PlayerPoints(PlayerIndex.One), new PlayerPoints(PlayerIndex.Two), new PlayerPoints(PlayerIndex.Three), new PlayerPoints(PlayerIndex.Four) };

        public void Init(int _killPoints, int _deathPoints, int _pointsToWin)
        {
            AddPoints = _killPoints;
            SubPoints = _deathPoints;
            pointsToWin = _pointsToWin;
        }

        public void UpdateKillPoints(PlayerIndex _killer, PlayerIndex _victim)
        {
            foreach (var item in pointsManager)
            {
                if (item.PlayerIndex == _killer)
                {
                    item.KillPoints += AddPoints;
                    Debug.Log(item.KillPoints);

                    if (item.KillPoints == pointsToWin)
                    {
                        if (OnPlayerWinnig != null)
                            OnPlayerWinnig(_killer);
                    }
                    break;
                }
            }

            foreach (var item in pointsManager)
            {
                if (item.PlayerIndex == _victim && item.KillPoints > 0)
                {
                    item.KillPoints -= SubPoints;
                    break;
                }
            }
        }

        public void UpdateKillPoints(PlayerIndex _victim)
        {
            foreach (var item in pointsManager)
            {
                if (item.PlayerIndex == _victim && item.KillPoints > 0)
                {
                    item.KillPoints -= SubPoints;
                    break;
                }
            }
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

        public void HandleAgentKilled(Agent _killer, Agent _victim)
        {
            if (_killer != null)
                UpdateKillPoints(_killer.playerIndex, _victim.playerIndex);           // setta i punti morte e uccisione
            else
                UpdateKillPoints(_victim.playerIndex);
        }
        #endregion
    }

    /// <summary>
    /// Contenitore dei punti del player
    /// </summary>
    public class PlayerPoints
    {

        PlayerIndex playerIndex;
        int powerPoints;
        int killPoints;

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

        public PlayerPoints(PlayerIndex _playerIndex)
        {
            playerIndex = _playerIndex;
        }

    }
}
