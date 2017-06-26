using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class StoreController : BaseMenu
    {
        public RectTransform ActiveImage;

        ModelPreviewContainer modelPreviewCtrl;
        SkinPreviewContainer skinPreviewCtrl;

        BaseMenu currentPanelActive;

        public void Init()
        {
            modelPreviewCtrl = GetComponentInChildren<ModelPreviewContainer>();
            skinPreviewCtrl = GetComponentInChildren<SkinPreviewContainer>();
            modelPreviewCtrl.Init(this);
            skinPreviewCtrl.Init(this);
            ActiveModelPanel();
        }

        #region Menu Actions
        public override void GoUpInMenu(Player _player)
        {
            currentPanelActive.GoUpInMenu(_player);
        }

        public override void GoDownInMenu(Player _player)
        {
            currentPanelActive.GoDownInMenu(_player);
        }

        public override void Selection(Player _player)
        {
            currentPanelActive.Selection(_player);
        }

        public override void GoBack(Player _player)
        {
            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.Blocked);
            GameManager.Instance.LoadingCtrl.ActivateLoadingPanel(() => {
                GameManager.Instance.flowSM.SetPassThroughOrder(new List<StateBase>() { new MainMenuState() });
            });
            if (EventManager.OnMenuAction != null)
                EventManager.OnMenuAction(AudioManager.UIAudio.Back);
        }

        public override void GoRightInMenu(Player _player)
        {
            if(currentPanelActive.GetComponent<SkinPreviewContainer>() == null)
                ActiveSkinPanel();
        }

        public override void GoLeftInMenu(Player _player)
        {
            if (currentPanelActive.GetComponent<ModelPreviewContainer>() == null)
                ActiveModelPanel();
        }

        #endregion

        void ActiveModelPanel()
        {
            modelPreviewCtrl.gameObject.SetActive(true);
            skinPreviewCtrl.gameObject.SetActive(false);
            currentPanelActive = modelPreviewCtrl;
            modelPreviewCtrl.IndexSelected = 0;
        }

        void ActiveSkinPanel()
        {
            modelPreviewCtrl.gameObject.SetActive(false);
            skinPreviewCtrl.gameObject.SetActive(true);
            currentPanelActive = skinPreviewCtrl;
            skinPreviewCtrl.IndexSelected = 0;
        }

        public void MoveActiveImage(RectTransform _position)
        {
            ActiveImage.position = _position.position;
        }
    }
}