using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{

    public class PlayerUpgradeController : BaseMenu
    {
        public UpgradeControllerID MenuID;

        UpgradeMenuManager UpgradeMng;

        public GameObject selectableUpgradePrefab;
        public Text PlayerIdText;
        public Text ConfirmText;

        UpgradeControllerState _currentState;
        public UpgradeControllerState CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                if(_currentState == UpgradeControllerState.Ready)
                {
                    UpgradeMng.CheckUpgradeControllersState();
                    GameManager.Instance.PlayerMng.ChangePlayerState(PlayerState.Blocked, Player.ID);
                    ConfirmText.text = "Ready";
                }
            }
        }

        public List<IUpgrade> Upgrades
        {
            get { return Player.Avatar.Upgrades; }
        }

        public void Setup(UpgradeMenuManager _upgradeMng, Player _player)
        {
            UpgradeMng = _upgradeMng;
            Player = _player;
        }

        /// <summary>
        /// Crea la parte grafica degli upgrades
        /// </summary>
        //void CreateUpgradeSliders()
        //{
        //    foreach (IUpgrade upgrade in Player.Avatar.Upgrades)
        //    {
        //        Instantiate(selectableUpgradePrefab, new Vector3(0,157,0), Quaternion.identity, GetComponent<VerticalLayoutGroup>().transform);
        //    }
        //}

        public void Init()
        {
            for (int i = 0; i < SelectableButtons.Count && i < Upgrades.Count; i++)
                (SelectableButtons[i] as ISelectableUpgrade).SetIUpgrade(Upgrades[i]); 
            CurrentState = UpgradeControllerState.Unready;
            ConfirmText.text = "Press A to continue";
            PlayerIdText.text = "Player " + Player.ID;
        }

        public override void Selection(Player _player)
        {
            for (int i = 0; i < Upgrades.Count; i++)
                Upgrades[i] = (SelectableButtons[i] as ISelectableUpgrade).GetData();
            CurrentState = UpgradeControllerState.Ready;
        }

        public override void GoRightInMenu(Player _player)
        {
            (selectableButton[currentIndexSelection] as SelectableUpgrade).AddValue();
        }

        public override void GoLeftInMenu(Player _player)
        {
            (selectableButton[currentIndexSelection] as SelectableUpgrade).RemoveValue();
        }
    }

    /// <summary>
    /// Serve per associare al upgrade controller il player corrispondente
    /// </summary>
    public enum UpgradeControllerID
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4
    }

    public enum UpgradeControllerState
    {
        Unready = 0,
        Ready = 1
    }
}