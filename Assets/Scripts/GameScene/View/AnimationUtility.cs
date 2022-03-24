using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WBTween;


public interface IAnimationPlayer
{
    public void Play();
}

public class AnimationUtility :MonoSingleton<AnimationUtility>
{
    [SerializeField] private GameObject winWindow;
    [SerializeField] private GameObject loseWindow;

    [SerializeField] private GameObject yourTurnWindow;
    [SerializeField] private GameObject rivalTurnWindow;

    [SerializeField] private WBTransition.Scene homeScene;

    public static bool IsAnimating => AnimationQueue.I.IsAnimating;

    public static void SetMovingAnimation(Transform cardTrn, ICardInfo card)
    {
        switch (card.Pos.Value)
        {
            case Pos.Deck:
                _setMovingAnimation(cardTrn, card);
                break;
            case Pos.Hand:
                if (card.IsPlayer)
                {
                    SetDrawAnimation(cardTrn, card);
                }
                else
                {
                    _setMovingAnimation(cardTrn, card);
                }
                break;
            case Pos.Field:
                _setMovingAnimation(cardTrn, card);
                break;
            case Pos.Discard:
                _setMovingAnimation(cardTrn, card);
                break;
            case Pos.Hero:
                _setMovingAnimation(cardTrn, card);
                break;
            default:
                break;
        }
    }

    internal static void ChangeTurn(bool isPlayer)
    {
        GameObject go = isPlayer?I.yourTurnWindow:I.rivalTurnWindow;

        Add(new Sequence(I).AppendCallback(() => go.SetActive(true)).Append(new Wait(1)).AppendCallback(() => go.SetActive(false)));
    }


    /// <summary>
    /// カードをフィールドに移動させるアニメーション
    /// </summary>
    /// <param name="cardTrn"></param>
    /// <param name="card"></param>
    public static void _setMovingAnimation(Transform cardTrn, ICardInfo card)
    {
        Sequence s = new Sequence(I);
        s.Append(
            new AppendCallback(() =>
            {
                Transform parent = PosViewLocator.I.Resolve(card.IsPlayer, card.Pos.Value).transform;
                if (cardTrn.parent.GetComponent<CircleAlign>() is CircleAlign ca)
                {
                    ca.Remove(cardTrn);
                }

                cardTrn.SetParent(parent, false);
                cardTrn.SetSiblingIndex(card.PositionIndex);
            })
        );


        Add(s);
    }

    /// <summary>
    /// カードを手札に移動させるアニメーション
    /// </summary>
    /// <param name="cardTrn"></param>
    /// <param name="card"></param>
    public static void SetDrawAnimation(Transform cardTrn, ICardInfo card)
    {
        Tuple<Vector3, Quaternion> t = null;
        var s = new Sequence(I);

        var hand = PosViewLocator.I.Resolve(card.IsPlayer, Pos.Hand);
        t = hand.GetComponent<CircleAlign>().AddTransform(cardTrn);
        cardTrn.rotation = t.Item2;


        s.Append(new Tween(cardTrn, ()=>t.Item1, 0.5f, true));


        Add(s);
    }

    /// <summary>
    /// カードを捨て札に移動させるアニメーション
    /// </summary>
    /// <param name="cardTrn"></param>
    /// <param name="card"></param>
    public static void SetDeadAnimation(Transform cardTrn, ICardInfo card)
    {
        Add(
            new AppendCallback(() => cardTrn.SetParent(PosViewLocator.I.Resolve(card.IsPlayer, Pos.Discard).transform))
        );
    }

    /// <summary>
    /// コストを変更させるアニメーション
    /// </summary>
    /// <param name="costText"></param>
    /// <param name="card"></param>
    public static void SetCostAnimation(TextMeshProUGUI costText, ICardInfo card)
    {
        Add(
            new AppendCallback(() => costText.text = card.Cost.Value.ToString())
        );
    }

    /// <summary>
    /// Hpを変更させるアニメーション
    /// </summary>
    /// <param name="hpText"></param>
    /// <param name="card"></param>
    public static void SetHpAnimation(TextMeshProUGUI[] hpText, IBattlerInfo card)
    {
        Add(
            new AppendCallback(() => hpText.ForEach(t=>t.text = card.Hp.Value.ToString()))
        );
    }

    /// <summary>
    /// Atkを変更させるアニメーション
    /// </summary>
    /// <param name="atkText"></param>
    /// <param name="card"></param>
    public static void SetAtkAnimation(TextMeshProUGUI[] atkText, IBattlerInfo card)
    {
        Add(
            new AppendCallback(() => atkText.ForEach(t=>t.text = card.Atk.Value.ToString()))
        );
    }

    /// <summary>
    /// ゲーム終了アニメーション
    /// </summary>
    /// <param name="playerWin"></param>
    public static void GameSet(bool playerWin)
    {

        Sequence s = new Sequence(I);
        s.Append(new Wait(1));
        s.AppendCallback(() => (playerWin ? I.winWindow : I.loseWindow).SetActive(true));
        s.Append(new Wait(3));
        s.AppendCallback(() => {
            I.StopAllCoroutines();
            WBTransition.SceneManager.LoadScene(I.homeScene);
        });
        Add(s);
    }

    private static void Add(TweenBase anim)
    {
        AnimationQueue.I.Add(new Sequence(I).Append(anim));
    }

    private static void Add(Sequence sequence)
    {
        AnimationQueue.I.Add(sequence);
    }


}
