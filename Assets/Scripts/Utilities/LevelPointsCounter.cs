using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

namespace BlackFox
{
    public class LevelPointsCounter
    {
        int AddPoints = 1;
        int SubPoints = 1;
        int PointsToWin = 5;

        List<PlayerStats> playerStats = new List<PlayerStats>()
        {   new PlayerStats(PlayerIndex.One),
            new PlayerStats(PlayerIndex.Two),
            new PlayerStats(PlayerIndex.Three),
            new PlayerStats(PlayerIndex.Four)
        };

        public LevelPointsCounter(int _addPoints, int _subPoints, int _pointsToWin)
        {
            AddPoints = _addPoints;
            SubPoints = _subPoints;
            PointsToWin = _pointsToWin;
        }

        /// <summary>
        /// Aggiorna i punti uccsione del player che è stato ucciso e di quello che ha ucciso
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        void AddKillPoints(PlayerIndex _killer)
        {
            foreach (PlayerStats player in playerStats)
            {
                if (player.PlayerIndex == _killer)
                {
                    player.KillPoints += AddPoints;

                    if (player.KillPoints == PointsToWin)
                    {
                        player.Victories += 1;
                        PlayerWin();
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Aggiorna i punti uccisione del player che è morto
        /// </summary>
        /// <param name="_victim"></param>
        void SubKillPoints(PlayerIndex _victim)
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

        /// <summary>
        /// Funzione chiamata alla vittoria del player
        /// </summary>
        void PlayerWin()
        {
            GameManager.Instance.LevelMng.PlayerWin();
        }

        #region API
        public void UpdateKillPoints(PlayerIndex _killer, PlayerIndex _victim)
        {
            AddKillPoints(_killer);
            SubKillPoints(_victim);
        }

        public void UpdateKillPoints(PlayerIndex _victim)
        {
            SubKillPoints(_victim);
        }

        /// <summary>
        /// Ritorna i punti uccisione del player che chiama la funzione
        /// </summary>
        /// <param name="_playerIndex">Indice del Player</param>
        /// <returns></returns>
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

        /// <summary>
        /// Azzera i punti uccisione di tutti i player
        /// </summary>
        public void ClearAllKillPoints()
        {
            foreach (PlayerStats player in playerStats)
            {
                player.ResetKillPoints();
            }
        }
        #endregion
    }


    /// <summary>
    /// Contenitore dei punti del player
    /// </summary>
    public class PlayerStats
    {
        PlayerIndex playerIndex;
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

        public int Victories
        {
            get { return victories; }
            set { victories = value; }
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