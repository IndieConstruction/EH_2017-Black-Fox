using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BlackFox
{
    public class Counter : MonoBehaviour
    {
        public Image CounterLable;
        [HideInInspector]
        public Sprite RoundNumber;
        public Sprite Img1;
        public Sprite Img2;
        public Sprite Img3;


        public void DoCountDown()
        {
            CounterLable.sprite = RoundNumber;
            transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
            {
                transform.localScale = Vector3.zero;
                CounterLable.sprite = Img3;
                transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
                {
                    transform.localScale = Vector3.zero;
                    CounterLable.sprite = Img2;
                    transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
                    {
                        transform.localScale = Vector3.zero;
                        CounterLable.sprite = Img1;
                        transform.DOScale(new Vector3(1f, 1f, 1f), 1f).OnComplete(() =>
                        {
                            transform.localScale = Vector3.zero;
                            transform.DOScale(new Vector3(0f, 0f, 0f), 0.5f).OnComplete(() =>
                            {
                                GameManager.Instance.LevelMng.gameplaySM.CurrentState.OnStateEnd();
                            }).SetEase(Ease.InExpo);
                        }).SetEase(Ease.OutBounce);
                    }).SetEase(Ease.OutBounce);
                }).SetEase(Ease.OutBounce);
            }).SetEase(Ease.OutBounce);
        }
    }
}