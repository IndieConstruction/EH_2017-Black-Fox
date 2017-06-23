﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class GameUIController : MonoBehaviour
    {
        List<PlayerHud> Huds = new List<PlayerHud>();

        public Slider ElementZeroSlider;

        public Text LevelIndicationText;

        public Text CoinCollectedText;

        List<GameObject> getHudPlayers()
        {
            List<GameObject> tempHudPlayers = new List<GameObject>();
            foreach (RectTransform item in GetComponentsInChildren<RectTransform>())
            {
                if(item.tag == "HUDPlayer")
                {
                    tempHudPlayers.Add(item.gameObject);
                }
            }
            return tempHudPlayers;
        }



        #region API

        public void Init()
        {
            List<Player> players = GameManager.Instance.PlayerMng.Players;
            List<GameObject> HudPlayer = getHudPlayers();
            for (int i = 0; i < players.Count; i++)
            {
                PlayerHud temphud = new PlayerHud();
                temphud.player = players[i];
                temphud.Hud = HudPlayer[i];
                temphud.GetImage().sprite = players[i].AvatarData.ColorSets[players[i].AvatarData.ColorSetIndex].Color.HudColor;
                Huds.Add(temphud);
            }
        }

        /// <summary>
        /// Quando viene richiamata va a leggere i punti del player richiesto nel levelMng e li aggiorna nella UI.
        /// </summary>
        /// <param name="_player">Il giocatore a cui aggiornare i punti uccisione nella Ui</param>
        public void SetKillPointsUI(Player _player)
        {
            for (int i = 0; i < Huds.Count; i++)
            {
                if (Huds[i].player == _player)
                    Huds[i].GetText().text = GameManager.Instance.LevelMng.GetPlayerKillPoints(_player.ID).ToString();
            }
        }

        public void ResetKillPointsUI()
        {
            for (int i = 0; i < Huds.Count; i++)
                Huds[i].GetText().text = "0";
        }

        public void SetElementZeroSlider(float _life, float _maxLife)
        {
            ElementZeroSlider.value = _life / _maxLife;                  // Da rivedere se il valore della vita cambia
        }

        /// <summary>
        /// Aggiorna il numero del livello e del round visualizzati durante il round
        /// </summary>
        public void UpdateLevelInformation()
        {
            LevelIndicationText.text = "Level: " + GameManager.Instance.LevelMng.CurrentLevel.LevelNumber + "/" 
                + "Round: " + GameManager.Instance.LevelMng.RoundNumber;
        }

        #endregion

        class PlayerHud
        {
            public Player player;
            public GameObject Hud;

            public Text GetText()
            {
                return Hud.GetComponentInChildren<Text>();
            }

            public Image GetImage()
            {
                return Hud.GetComponentInChildren<Image>();
            }

        }

    }
}
