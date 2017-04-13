using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class Counter : MonoBehaviour
    {
        public Text CounterLable;

        public void DoCountDown()
        {
            CounterLable.text = "3";
            transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
            {
                transform.localScale = Vector3.zero;
                CounterLable.text = "2";
                transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
                {
                    transform.localScale = Vector3.zero;
                    CounterLable.text = "1";
                    transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
                    {
                        transform.localScale = Vector3.zero;
                        CounterLable.text = "GO!!!";
                        transform.DOScale(new Vector3(1f, 1f, 1f), 0.6f).OnComplete(() =>
                        {
                            GameManager.Instance.PlayerMng.ChangeAllPlayersState(PlayerState.PlayInputState);
                            transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f).OnComplete(() =>
                            {
                                if (OnCounterEnded != null)
                                    OnCounterEnded();
                            }).SetEase(Ease.InExpo);
                        }).SetEase(Ease.OutBounce);
                    }).SetEase(Ease.OutBounce);
                }).SetEase(Ease.OutBounce);
            }).SetEase(Ease.OutBounce);

        }

        #region LevelEvent
        public delegate void CounterEvent();

        /// <summary>
        /// Evento chiamato alla fine di un counter.
        /// </summary>
        public static CounterEvent OnCounterEnded;
        #endregion
    }
}