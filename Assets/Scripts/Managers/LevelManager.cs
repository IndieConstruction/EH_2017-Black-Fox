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
        public int levelNumber;

        public int AddPoints = 1;
        public int SubPoints = 1;
        public int pointsToWin = 5;

        public GameObject SpawnerMngPrefab;
        public GameObject RopeMngPrefab;

        [HideInInspector]
        public SpawnerManager spawnerMng;
        [HideInInspector]
        public RopeManager ropeMng;

        public Level currentLevel;

        GameplaySM gameplaySM;

        void Start()
        {
            currentLevel = Instantiate(Resources.Load<Level>("Prefabs/Levels/Level" + levelNumber));
            StartGameplaySM();
        }

        #region API
        /// <summary>
        /// Instance a preloaded SpawnManager
        /// </summary>
        public void InstantiateSpawnerManager()
        {
            spawnerMng = Instantiate(SpawnerMngPrefab, transform).GetComponent<SpawnerManager>();
            //spawnerMng.Init(levelNumber, roundNumber, currentLevel.LevelSpawners);

            currentLevel.ArrowsSpawner.CreateInstance(currentLevel.ArrowsSpawner, spawnerMng.transform);
            currentLevel.AvatarSpawner.CreateInstance(currentLevel.AvatarSpawner, spawnerMng.transform);
            currentLevel.BlackHoleSpawner.CreateInstance(currentLevel.BlackHoleSpawner, spawnerMng.transform);
            currentLevel.ExternalElementSpawner.CreateInstance(currentLevel.ExternalElementSpawner, spawnerMng.transform);
            currentLevel.WaveSpawner.CreateInstance(currentLevel.WaveSpawner, spawnerMng.transform);
        }
        /// <summary>
        /// Instance a preloaded RopeManager
        /// </summary>
        public void InstantiateRopeManager()
        {
            ropeMng = Instantiate(RopeMngPrefab, transform).GetComponent<RopeManager>();
        }

        /// <summary>
        /// Carica lo scriptable object del livello e istanzia il prefab del livello
        /// </summary>
        public void InstantiateArena()
        {
            Instantiate(currentLevel.ArenaPrefab, transform);
        }

        /// <summary>
        /// Inizializza lo spawner manager
        /// </summary>
        public void InitSpawnerManager()
        {
            spawnerMng.InitLevel();
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
        #endregion

        #region KillPoint Count

        List<PlayerStats> playerStats = new List<PlayerStats>()
        {   new PlayerStats(PlayerIndex.One),
            new PlayerStats(PlayerIndex.Two),
            new PlayerStats(PlayerIndex.Three),
            new PlayerStats(PlayerIndex.Four)
        };

        /// <summary>
        /// Aggiorna i punti uccsione del player che è stato ucciso e di quello che ha ucciso
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
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
                        PlayerWin();
                    }
                    break;
                }
            }

            UpdateKillPoints(_victim);
        }

        /// <summary>
        /// Aggiorna i punti uccisione del player che è morto
        /// </summary>
        /// <param name="_victim"></param>
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


        /// <summary>
        /// Funzione che contiene le azione da eseguire alla morte del player
        /// </summary>
        void PlayerWin()
        {
            roundNumber++;
            gameplaySM.SetRoundNumber(roundNumber);
            ClearKillPoints();
            EventManager.OnPlayerWinnig();
        }

        /// <summary>
        /// Azzera i punti uccisione di tutti i player
        /// </summary>
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
            EventManager.OnAgentKilled += HandleOnAgentKilled;
            EventManager.OnCoreDeath += HandleOnCoreDeath;
            EventManager.OnAgentSpawn += HandleOnAgentSpawn;
        }

        private void OnDisable()
        {
            EventManager.OnAgentKilled -= HandleOnAgentKilled;
            EventManager.OnCoreDeath -= HandleOnCoreDeath;
            EventManager.OnAgentSpawn -= HandleOnAgentSpawn;

        }

        #region Event Handler
        /// <summary>
        /// Viene chiamata quando accade un'uccisione.
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        void HandleOnAgentKilled(Agent _killer, Agent _victim)
        {
            if (_killer != null)
            {
                UpdateKillPoints(_killer.playerIndex, _victim.playerIndex);           // setta i punti morte e uccisione
                _killer.UpdateKillPointsInUI(_killer.playerIndex, GetPlayerKillPoints(_killer.playerIndex));
                _victim.UpdateKillPointsInUI(_victim.playerIndex, GetPlayerKillPoints(_victim.playerIndex));
            }
            else
                UpdateKillPoints(_victim.playerIndex);
            if (EventManager.OnPointsUpdate != null)
            {
                EventManager.OnPointsUpdate();
            }
            //Reaction of the RopeManager to the OnAgentKilled event
            ropeMng.ReactToOnAgentKilled(_victim);
            //Reaction of the SpawnerManager to the OnAgentKilled event
            spawnerMng.ReactToOnAgentKilled(_killer, _victim);
        }

        /// <summary>
        /// Viene chiamata quando muore il core
        /// </summary>
        void HandleOnCoreDeath()
        {
            ClearKillPoints();
            spawnerMng.InitLevel();
            //Reaction of the RopeManager to the OnCoreDeath event
            ropeMng.ReactToOnCoreDeath();
        }

        /// <summary>
        /// Viene chiamamta alla morte di un agente
        /// </summary>
        /// <param name="_agent"></param>
        void HandleOnAgentSpawn(Agent _agent)
        {
            //Reaction of the RopeManager to the OnAgentSpawn event
            ropeMng.ReactToOnAgentSpawn(_agent);
        }
        #endregion

        #endregion

        #region GameplaySM

        /// <summary>
        /// Istaniuzia la GameplaySM e passa i parametri di livello e round corretni e MaxRound alla state machine
        /// </summary>
        void StartGameplaySM()
        {
            gameplaySM = gameObject.AddComponent<GameplaySM>();
            gameplaySM.SetLevelNumber(levelNumber);
            gameplaySM.SetMaxRoundNumber(MaxRound);
            gameplaySM.SetRoundNumber(roundNumber);
        }

        #endregion

        /// <summary>
        /// Pulisce l'arena dagli oggetti del round precedente
        /// </summary>
        void ClearArena()
        {
            GameObject[] pins = GameObject.FindGameObjectsWithTag("Pin");

            foreach (GameObject pin in pins)
            {
                GameObject.Destroy(pin);
            }
        }
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
