using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public abstract class PositionView : MonoBehaviour
{
    [SerializeField] protected bool isPlayer;
    public bool IsPlayer => isPlayer;



    /// <summary>
    /// キャラカードのプレハブが存在する場所
    /// </summary>
    protected virtual string PrefabPathForDebugging => "CharacterCard";

    public void Register()
    {
        PosViewLocator.I.Register(this);
    }

    public virtual void Initialize()
    {
        PositionLocatorInfo.I.Resolve(IsPlayer, Pos).CardMade.Subscribe(c =>
        {
            MakeCard(c);
        });
    }

    public abstract Pos Pos { get; }

    /// <summary>
    /// このポジションにカードのゲームオブジェクトを生成する
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    private void MakeCard(ICardInfo status)
    {
        //カードのプレハブをインスタンス化
        //カードの種類によって、用いるプレハブが異なる
        GameObject cardObj = Instantiate(Resources.Load<GameObject>(PrefabPathForDebugging), transform, false);

        CardVisual debugVisual = cardObj.GetComponent<CardVisual>();

        //分かりやすいように、オブジェクト名としてカードの名前を用いる
        cardObj.name = status.CardBook.CardName;

        debugVisual.Initialize(status);
        int n = status.PositionIndex;
        cardObj.transform.SetSiblingIndex(n);

        if (n == 2)
        {
            cardObj.transform.SetAsFirstSibling();

        }
    }



}
