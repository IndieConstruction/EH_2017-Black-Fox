using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

namespace BlackFox
{
    public class RoundController : MonoBehaviour
    {
        public int KillPoint;
        public int DeathPoint;
        public int PointsToWin;

        PointsManager pointsManager;

        void Start()
        {
            pointsManager = new PointsManager(KillPoint, DeathPoint, PointsToWin);
            PointsManager.OnPlayerWinnig += OnPlayerWinnig;
        }

        #region PointsManager
        public void AgentKilled(Agent _killer, Agent _victim)
        {
            if (_killer != null)
                pointsManager.UpdateKillPoints(_killer.playerIndex, _victim.playerIndex);           // setta i punti morte e uccisione
            else
                pointsManager.UpdateKillPoints(_victim.playerIndex);
        }

        void OnPlayerWinnig(PlayerIndex _winner)
        {
            // TODO : aggiungere funzione in caso di vincita del player
        }
        #endregion
    }
}