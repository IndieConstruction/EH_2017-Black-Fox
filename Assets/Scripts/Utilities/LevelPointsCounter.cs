using System.Collections;
using System.Collections.Generic;

namespace BlackFox
{
    public class LevelPointsCounter
    {
        LevelManager levelManager;

        int AddPoints
        {
            get { return levelManager.levelOptions.AddPoints; }
        }
        int SubPoints
        {
            get { return levelManager.levelOptions.SubPoints; }
        }
        int PointsToWin
        {
            get { return levelManager.levelOptions.PointsToWin; }
        }
        int VictoriesToWin
        {
            get { return levelManager.levelOptions.VictoriesToWin; }
        }

        List<PlayerStats> playerStats = new List<PlayerStats>()
        {   new PlayerStats(PlayerLabel.One),
            new PlayerStats(PlayerLabel.Two),
            new PlayerStats(PlayerLabel.Three),
            new PlayerStats(PlayerLabel.Four)
        };

        public LevelPointsCounter(LevelManager _levelManager)
        {
            levelManager = _levelManager;
        }

        /// <summary>
        /// Aggiorna i punti uccsione del player che è stato ucciso e di quello che ha ucciso
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        void AddKillPoints(PlayerLabel _killer)
        {
            foreach (PlayerStats player in playerStats)
            {
                if (player.PlayerID == _killer)
                {
                    player.KillPoints += AddPoints;

                    if (player.KillPoints == PointsToWin)
                    {
                        player.Victories += 1;
                        GameManager.Instance.LevelMng.UpgradePointsMng.GivePoints(player.PlayerID);
                        GameManager.Instance.LevelMng.PlayerWin(player.PlayerID.ToString());
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Aggiorna i punti uccisione del player che è morto
        /// </summary>
        /// <param name="_victim"></param>
        void SubKillPoints(PlayerLabel _victim)
        {
            foreach (PlayerStats player in playerStats)
            {
                if (player.PlayerID == _victim && player.KillPoints > 0)
                {
                    player.KillPoints -= SubPoints;
                    break;
                }
            }
        }


        #region API
        public void UpdateKillPoints(PlayerLabel _killer, PlayerLabel _victim)
        {
            AddKillPoints(_killer);
            SubKillPoints(_victim);
        }

        public void UpdateKillPoints(PlayerLabel _victim)
        {
            SubKillPoints(_victim);
        }

        /// <summary>
        /// Ritorna i punti uccisione del player che chiama la funzione
        /// </summary>
        /// <param name="_playerID">Indice del Player</param>
        /// <returns></returns>
        public int GetPlayerKillPoints(PlayerLabel _playerID)
        {
            foreach (PlayerStats player in playerStats)
                if (player.PlayerID == _playerID)
                    return player.KillPoints;
            return -1;
        }

        /// <summary>
        /// Ritorna le vittorie del player che chiama la funzione
        /// </summary>
        /// <param name="_playerID"></param>
        /// <returns></returns>
        public int GetPlayerVictories(PlayerLabel _playerID)
        {
            foreach (PlayerStats player in playerStats)
                if (player.PlayerID == _playerID)
                    return player.Victories;
            return -1;
        }

        /// <summary>
        /// Ritorna true se c'è un player che ha abbastanza vitorie per vincere
        /// </summary>
        /// <returns></returns>
        public bool CheckNumberVictories()
        {
            foreach (PlayerStats player in playerStats)
                if (player.Victories == VictoriesToWin)
                    return true;
            return false;
        }

        /// <summary>
        /// Ritorna true se all'ulitmo round ci sono due player con le stesse vittorie
        /// </summary>
        /// <returns></returns>
        public bool CheckPlayerWithSameVictories()
        {
            if (levelManager.RoundNumber == levelManager.levelOptions.MaxRound)
            {
                for (int i = 0; i < playerStats.Count; i++)
                {
                    for (int j = 0; j < playerStats.Count; j++)
                    {
                        if (playerStats[j].PlayerID != playerStats[i].PlayerID)
                            if (playerStats[j].Victories == playerStats[i].Victories)
                                return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Azzera i punti uccisione di tutti i player
        /// </summary>
        public void ClearAllKillPoints()
        {
            foreach (PlayerStats player in playerStats)
                player.ResetKillPoints();
        }

        /// <summary>
        /// Azzera i punti uccisione di tutti i player
        /// </summary>
        public void ClearAllVictories()
        {
            foreach (PlayerStats player in playerStats)
                player.ResetVictories();
        }
        #endregion
    }

    /// <summary>
    /// Contenitore dei punti del player
    /// </summary>
    public class PlayerStats
    {
        PlayerLabel playerID;
        int killPoints;
        int victories;

        public PlayerLabel PlayerID
        {
            get { return playerID; }
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

        public PlayerStats(PlayerLabel _playerIndex)
        {
            playerID = _playerIndex;
        }

        public void ResetKillPoints()
        {
            killPoints = 0;
        }

        public void ResetVictories()
        {
            Victories = 0;
        }
    }
}