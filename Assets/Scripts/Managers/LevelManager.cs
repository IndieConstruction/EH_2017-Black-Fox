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
        public int PointsToWin = 5;

        public GameObject SpawnerMngPrefab;
        public GameObject RopeMngPrefab;

        [HideInInspector]
        public SpawnerManager SpawnerMng;
        [HideInInspector]
        public RopeManager RopeMng;
        [HideInInspector]
        public Level CurrentLevel;
        [HideInInspector]
        public Core Core;
        [HideInInspector]
        public GameObject Arena;


        GameplaySM gameplaySM;
        LevelPointsCounter levelPointsCounter;

        #region Containers
        public Transform PinsContainer;
        #endregion

        void Start()
        {
            CurrentLevel = Instantiate(Resources.Load<Level>("Levels/Level" + levelNumber));
            StartGameplaySM();
            levelPointsCounter = new LevelPointsCounter(AddPoints, SubPoints, PointsToWin);
        }

        #region API
        #region Instantiation
        /// <summary>
        /// Instance a preloaded SpawnManager
        /// </summary>
        public void InstantiateSpawnerManager()
        {
            SpawnerMng = Instantiate(SpawnerMngPrefab, transform).GetComponent<SpawnerManager>();

            CurrentLevel.ArrowsSpawner.CreateInstance(CurrentLevel.ArrowsSpawner, SpawnerMng.transform);
            CurrentLevel.AvatarSpawner.CreateInstance(CurrentLevel.AvatarSpawner, SpawnerMng.transform);
            CurrentLevel.BlackHoleSpawner.CreateInstance(CurrentLevel.BlackHoleSpawner, SpawnerMng.transform);
            CurrentLevel.ExternalElementSpawner.CreateInstance(CurrentLevel.ExternalElementSpawner, SpawnerMng.transform);
            CurrentLevel.TurretSpawner.CreateInstance(CurrentLevel.TurretSpawner, SpawnerMng.transform);
            CurrentLevel.WaveSpawner.CreateInstance(CurrentLevel.WaveSpawner, SpawnerMng.transform);
        }
        /// <summary>
        /// Instance a preloaded RopeManager
        /// </summary>
        public void InstantiateRopeManager()
        {
            RopeMng = Instantiate(RopeMngPrefab, transform).GetComponent<RopeManager>();
        }
        /// <summary>
        /// Carica lo scriptable object del livello e istanzia il prefab del livello
        /// </summary>
        public void InstantiateArena()
        {
            Arena = Instantiate(CurrentLevel.ArenaPrefab, transform);
            ResetPinsContainer(Arena.transform);
        }
        #endregion
        #region Initialization
        /// <summary>
        /// Inizializza lo spawner manager
        /// </summary>
        public void InitSpawnerManager()
        {
            SpawnerMng.InitLevel();
        }
        /// <summary>
        /// Inizializza il core
        /// </summary>
        public void InitCore()
        {
            if (Core != null)
                Core.Init();
        }
        #endregion

        public int GetPlayerKillPoints(PlayerIndex _playerIndex)
        {
            return levelPointsCounter.GetPlayerKillPoints(_playerIndex);
        }

        /// <summary>
        /// Funzione da eseguire alla morte del core
        /// </summary>
        public void CoreDeath()
        {
            levelPointsCounter.ClearAllKillPoints();
            EventManager.TriggerPlayStateEnd();
        }

        /// <summary>
        /// Funzione che contiene le azioni da eseguire alla vittoria del player
        /// </summary>
        public void PlayerWin()
        {
            roundNumber++;
            gameplaySM.SetRoundNumber(roundNumber);
            levelPointsCounter.ClearAllKillPoints();
            EventManager.TriggerPlayStateEnd();
        }

        /// <summary>
        /// Funzione che contiene le azioni da eseguire alla morte di un player
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        public void AgentKilled(Agent _killer, Agent _victim)
        {
            if (_killer != null)
            {
                levelPointsCounter.UpdateKillPoints(_killer.playerIndex, _victim.playerIndex);           // setta i punti morte e uccisione
                _killer.UpdateKillPointsInUI(_killer.playerIndex, levelPointsCounter.GetPlayerKillPoints(_killer.playerIndex));
                _victim.UpdateKillPointsInUI(_victim.playerIndex, levelPointsCounter.GetPlayerKillPoints(_victim.playerIndex));
                GameManager.Instance.uiManager.endRoundUI.AddKillPointToUI(_killer, _victim);
            }
            else
            {
                levelPointsCounter.UpdateKillPoints(_victim.playerIndex);
                _victim.UpdateKillPointsInUI(_victim.playerIndex, levelPointsCounter.GetPlayerKillPoints(_victim.playerIndex));
                GameManager.Instance.uiManager.endRoundUI.AddKillPointToUI(_killer, _victim);
            }
            if (EventManager.OnPointsUpdate != null)
            {
                EventManager.OnPointsUpdate();
            }
            //Reaction of the RopeManager to the OnAgentKilled event
            RopeMng.ReactToOnAgentKilled(_victim);
            //Reaction of the SpawnerManager to the OnAgentKilled event
            SpawnerMng.ReactToOnAgentKilled(_victim);
        }

        /// <summary>
        /// Funzione che contiene le azioni da eseguire al resapwn di un player
        /// </summary>
        /// <param name="_agent"></param>
        public void AgentSpawn(Agent _agent)
        {
            //Reaction of the RopeManager to the OnAgentSpawn event
            RopeMng.ReactToOnAgentSpawn(_agent);
        }

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

        #region Pins
        /// <summary>
        /// Destroy and Initialize a new PinsContainer
        /// </summary>
        void ResetPinsContainer(Transform _parent) {
            if (PinsContainer)
                Destroy(PinsContainer.gameObject);
            PinsContainer = new GameObject("PinsContainer").transform;
            PinsContainer.transform.parent = _parent;
        }
        /// <summary>
        /// Remove all Pins in Scene
        /// </summary>
        public void CleanPins() {
            ResetPinsContainer(Arena.transform);
        }
        #endregion
    }
}
