﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlayState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PlayState");
            GameManager.Instance.LevelMng.SpawnerMng.ToggleSpawners(true);
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.PlayInput);
            GameManager.Instance.LevelMng.RoundBegin();
            GameManager.Instance.PowerUpManager.Toggle(true);
            EventManager.OnAgentKilled += HandleOnAgentKilled;
        }

        public override void OnEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            EventManager.OnAgentKilled -= HandleOnAgentKilled;
        }

        #region Events Handler

        void HandleOnAgentKilled(Avatar _killer, Avatar _victim)
        {
            GameManager.Instance.LevelMng.UpdateKillPoints(_killer, _victim);
            GameManager.Instance.UiMng.canvasGame.gameUIController.SetKillPointsUI(_killer.Player.ID);
            GameManager.Instance.UiMng.canvasGame.gameUIController.SetKillPointsUI(_victim.Player.ID);
            if (GameManager.Instance.LevelMng.IsRoundActive)
                GameManager.Instance.LevelMng.AvatarSpwn.SpawnAvatar(_victim.Player, 3);

        }
        #endregion
    }
}
