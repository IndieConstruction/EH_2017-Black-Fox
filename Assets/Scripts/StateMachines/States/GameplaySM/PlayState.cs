using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlackFox
{
    public class PlayState : StateBase
    {
        public override void OnStart()
        {
            Debug.Log("PlayState");
            EventManager.TriggerPlayStateEnd += HandleTriggerPlayStateEnd;
            EventManager.OnAgentKilled += HandleOnAgentKilled;
        }

        public override void OnEnd()
        {
            // passaggio informazioni essenziali al gestore del livello
            EventManager.TriggerPlayStateEnd -= HandleTriggerPlayStateEnd;
            EventManager.OnAgentKilled -= HandleOnAgentKilled;
        }

        #region Events Handler
        void HandleTriggerPlayStateEnd()
        {
            OnStateEnd();
        }

        void HandleOnAgentKilled(Avatar _killer, Avatar _victim)
        {
            GameManager.Instance.LevelMng.UpdateKillPoints(_killer, _victim);
            GameManager.Instance.LevelMng.AvatarSpwn.RespawnAvatar(_victim.Player, 3);
            GameManager.Instance.UiMng.gameUIController.SetKillPointsUI(_killer.Player.ID);
            GameManager.Instance.UiMng.gameUIController.SetKillPointsUI(_victim.Player.ID);
        }
        #endregion
    }
}
