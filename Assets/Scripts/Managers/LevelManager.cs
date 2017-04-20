using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        public GameObject AvatarSpwnPrefab;
        public GameObject RopeMngPrefab;
        public GameManager gameMngr;
        [HideInInspector]
        public SpawnerManager SpawnerMng;
        [HideInInspector]
        public RopeManager RopeMng;
        [HideInInspector]
        public AvatarSpawner AvatarSpwn;
        [HideInInspector]
        public Level CurrentLevel;
        [HideInInspector]
        public Core Core;
        [HideInInspector]
        public GameObject Arena;
        
        [HideInInspector]
        public GameplaySM gameplaySM;

        [HideInInspector]
        public string EndLevelPanelLable;
        
        LevelPointsCounter levelPointsCounter;

        public bool IsGamePaused;

        #region Containers
        public Transform PinsContainer;
        #endregion

        void Start()
        {
            CurrentLevel = Instantiate(InstantiateLevel());
            StartGameplaySM();
            levelPointsCounter = new LevelPointsCounter(AddPoints, SubPoints, PointsToWin);
        }

        #region API

        #region Instantiation
        /// <summary>
        /// Funzione che ritorna lo scriptable del livello da caricare
        /// </summary>
        /// <returns></returns>
        public Level InstantiateLevel()
        {
            if (GameManager.Instance.LevelScriptableObj != null)
                return GameManager.Instance.LevelScriptableObj;
            else
                return Resources.Load<Level>("Levels/Level" + levelNumber);
        }


        /// <summary>
        /// Instance a preloaded SpawnManager
        /// </summary>
        public void InstantiateSpawnerManager()
        {
            SpawnerMng = Instantiate(SpawnerMngPrefab, transform).GetComponent<SpawnerManager>();

            SpawnerMng.InstantiateNewSpawners(CurrentLevel);
        }
        /// <summary>
        /// Instance a preloaded RopeManager
        /// </summary>
        public void InstantiateRopeManager()
        {
            RopeMng = Instantiate(RopeMngPrefab, transform).GetComponent<RopeManager>();
        }
        /// <summary>
        /// Istance a new AvatarSpawner
        /// </summary>
        public void InstantiateAvatarSpawner()
        {
            AvatarSpwn = Instantiate(AvatarSpwnPrefab, transform).GetComponent<AvatarSpawner>();
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

        #region Avatar
        /// <summary>
        /// Funzione che contiene le azioni da eseguire alla morte di un player
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        public void AgentKilled(Avatar _killer, Avatar _victim)
        {
            if (_killer != null)
            {
                levelPointsCounter.UpdateKillPoints(_killer.PlayerId, _victim.PlayerId);           // setta i punti morte e uccisione
            }
            else
            {
                levelPointsCounter.UpdateKillPoints(_victim.PlayerId);
            }
            if(EventManager.OnPointsUpdate != null)
                EventManager.OnPointsUpdate();
            
            //Reaction of the AvatarSpawner to the OnAgentKilled event
            AvatarSpwn.RespawnAvatar(_victim.Player, _victim.Player.SpawnTime);
        }
        /// <summary>
        /// Return the current points (due to kills) of the Player
        /// </summary>
        /// <param name="_playerID"></param>
        /// <returns></returns>
        public int GetPlayerKillPoints(PlayerLabel _playerID)
        {
            return levelPointsCounter.GetPlayerKillPoints(_playerID);
        }
        /// <summary>
        /// Destroy all Avatars
        /// </summary>
        public void DestroyAllAvatars()
        {
            foreach (Player player in gameMngr.PlayerMng.Players)
            {
                player.DestroyAvatar();
            }
        }
        /// <summary>
        /// Instance new avatars
        /// </summary>
        /// <param name="_spawnTime"></param>
        public void SpawnAllAvatar(float _spawnTime) {
            foreach (Player player in GameManager.Instance.PlayerMng.Players) {
                AvatarSpwn.RespawnAvatar(player, _spawnTime);
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Inizializza il core
        /// </summary>
        public void InitCore()
        {
            if (Core != null)
                Core.Init();
        }
        #endregion

        /// <summary>
        /// Funzione da eseguire alla morte del core
        /// </summary>
        public void CoreDeath()
        {
            levelPointsCounter.ClearAllKillPoints();
            EndLevelPanelLable = "Core Has Been Destroyed";
            EventManager.TriggerPlayStateEnd();
        }

        /// <summary>
        /// Funzione che contiene le azioni da eseguire alla vittoria del player
        /// </summary>
        public void PlayerWin(string _winner)
        {
            roundNumber++;
            gameplaySM.SetRoundNumber(roundNumber);
            levelPointsCounter.ClearAllKillPoints();
            EndLevelPanelLable = "Player " + _winner + " Has Won";
            EventManager.TriggerPlayStateEnd();
            CoinManager.Coins +=4;
            gameMngr.AddCoins();
        }

        /// <summary>
        /// Attiva lo stato di pausa della GameplaySM e imposta a menu input i comandi del player che ha chiamato la fuznione
        /// mentre l'input degli altri player viene disabilitato
        /// </summary>
        /// <param name="_playerID"></param>
        public void PauseGame(PlayerLabel _playerID)
        {
            // TODO : controllare uso corretto di if
            if (!IsGamePaused)
            {
                IsGamePaused = true;
                GameManager.Instance.PlayerMng.ChangeAllPlayersStateExceptOne(PlayerState.MenuInput, _playerID, PlayerState.Blocked);
                gameplaySM.GoToState(GamePlaySMStates.PauseState);
            }            
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
