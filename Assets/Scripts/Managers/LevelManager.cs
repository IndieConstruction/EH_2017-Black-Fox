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
        #region Prefabs
        public GameObject SpawnerMngPrefab;
        public GameObject AvatarSpwnPrefab;
        public GameObject RopeMngPrefab;
        #endregion

        #region Managers
        [HideInInspector]
        public SpawnerManager SpawnerMng;
        [HideInInspector]
        public RopeManager RopeMng;
        [HideInInspector]
        public AvatarSpawner AvatarSpwn;
        #endregion

        #region Level
        [HideInInspector]
        public Level CurrentLevel;
        [HideInInspector]
        public GameplaySM gameplaySM;
        [HideInInspector]
        public GameObject Arena;

        [HideInInspector]
        public Core Core;

        [HideInInspector]
        public string EndLevelPanelLableText;
        
        [HideInInspector]
        public LevelOptions levelOptions;

        public int LevelNumber
        {
            get { return CurrentLevel.LevelNumber; }
        }

        #region Round
        [HideInInspector]
        public int RoundNumber;
        [HideInInspector]
        public bool IsGamePaused;

        private bool _isRoundActive;
        /// <summary>
        /// Se true il round attuale è attivo.
        /// </summary>
        public bool IsRoundActive {
            get { return _isRoundActive; }
            set { _isRoundActive = value; }
        }
        #endregion

        #region Containers
        [HideInInspector]
        public Transform PinsContainer;
        #endregion

        #endregion

        LevelPointsCounter levelPointsCounter;

        #region API
        public void Init()
        {
            CurrentLevel = Instantiate(InstantiateLevel());
            levelOptions = CurrentLevel.LevelOptions;
            StartGameplaySM();
            levelPointsCounter = new LevelPointsCounter(this);
            RoundNumber = 1;
        }

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
                return Resources.Load<Level>("Levels/Level" + LevelNumber);
        }

        /// <summary>
        /// Instance a preloaded SpawnManager
        /// </summary>
        public void InstantiateSpawnerManager()
        {
            SpawnerMng = Instantiate(SpawnerMngPrefab, transform).GetComponent<SpawnerManager>();
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
            AvatarSpwn.Init();
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

        #region Level
        /// <summary>
        /// Funzione da eseguire alla morte del core
        /// </summary>
        public void CoreDeath()
        {
            EndLevelPanelLableText = "Core Has Been Destroyed";
            gameplaySM.CurrentState.OnStateEnd();
        }

        /// <summary>
        /// Funzione che contiene le azioni da eseguire alla vittoria del player
        /// </summary>
        public void PlayerWin(string _winner)
        {
            NextRound();
            EndLevelPanelLableText = "Player " + _winner + " Has Won";
            gameplaySM.CurrentState.OnStateEnd();
            IsRoundActive = false;
            CoinManager.coins += 4;
            CoinManager.AddCoins();
        }

        /// <summary>
        /// Attiva lo stato di pausa della GameplaySM e imposta a menu input i comandi del player che ha chiamato la fuznione
        /// mentre l'input degli altri player viene disabilitato
        /// </summary>
        /// <param name="_playerID"></param>
        public void PauseGame(PlayerLabel _playerID)
        {
            if (!IsGamePaused)
            {
                IsGamePaused = true;
                GameManager.Instance.PlayerMng.ChangeAllPlayersStateExceptOne(PlayerState.MenuInput, _playerID, PlayerState.Blocked);
                GameManager.Instance.LevelMng.gameplaySM.SetPassThroughOrder(new List<StateBase>() { new PauseState() });
            }
        }

        /// <summary>
        /// Avanza di round.
        /// </summary>
        public void NextRound()
        {
            RoundNumber++;
        }

        /// <summary>
        /// Chiamato quando inizia il round.
        /// </summary>
        public void RoundBegin()
        {
            IsRoundActive = true;
        }

        /// <summary>
        /// Azzera il contatore dei punti
        /// </summary>
        public void ClearPoints()
        {
            levelPointsCounter.ClearAllKillPoints();
        }
        #endregion

        #region Avatar
        /// <summary>
        /// Aggiorna i Kill point
        /// </summary>
        /// <param name="_killer"></param>
        /// <param name="_victim"></param>
        public void UpdateKillPoints(Avatar _killer, Avatar _victim)
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
        /// Instance new avatars
        /// </summary>
        /// <param name="_spawnTime"></param>
        public void SpawnAllAvatar(float _spawnTime)
        {
            //foreach (Player player in GameManager.Instance.PlayerMng.Players.Where(p => p.Avatar != null)) {
            foreach (Player player in GameManager.Instance.PlayerMng.Players)
                AvatarSpwn.SpawnAvatar(player, _spawnTime);
        }
        #endregion

        #region Initialization
        public void SetupCore()
        {
            Core = Arena.GetComponentInChildren<Core>();
            if(Core != null)
                Core.Setup();
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
        #endregion

        #region GameplaySM
        /// <summary>
        /// Istaniuzia la GameplaySM e passa i parametri di livello e round corretni e MaxRound alla state machine
        /// </summary>
        void StartGameplaySM()
        {
            gameplaySM = gameObject.AddComponent<GameplaySM>();
            gameplaySM.Init();
        }       
        #endregion

        #region Pins
        /// <summary>
        /// Destroy and Initialize a new PinsContainer
        /// </summary>
        void ResetPinsContainer(Transform _parent)
        {
            if (PinsContainer)
                Destroy(PinsContainer.gameObject);
            PinsContainer = new GameObject("PinsContainer").transform;
            PinsContainer.transform.parent = _parent;
        }

        /// <summary>
        /// Remove all Pins in Scene
        /// </summary>
        public void CleanPins()
        {
            ResetPinsContainer(Arena.transform);
        }
        #endregion
    }

    [Serializable]
    public class LevelOptions
    {
        public int MaxRound;
        public int AddPoints;
        public int SubPoints;
        public int PointsToWin;
    }
}
