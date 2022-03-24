using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enum Pos からIPositionのインスタンスを取得できるサービスロケータクラス
/// IPositionは、PositionBaseクラスのうち読み取り専用変数のみ公開するインターフェース
/// </summary>
public abstract class PositionLocatorInfo
{
    public static PositionLocatorInfo I { get; protected set; }

    /// <summary>
    /// プレイヤーのPositionクラス
    /// </summary>
    protected Dictionary<Type, IPosition> playerPositionMap = new Dictionary<Type, IPosition>();

    /// <summary>
    /// 対戦相手のPositionクラス
    /// </summary>
    protected Dictionary<Type, IPosition> rivalPositionMap = new Dictionary<Type, IPosition>();

    /// <summary>
    /// ポジションのインスタンスを取得する
    /// </summary>
    /// <param name="isPlayer">プレイヤーor対戦相手</param>
    /// <param name="pos">どのポジションのインスタンスが欲しいか</param>
    /// <returns>ポジションのインスタンス</returns>
    public IPosition Resolve(bool isPlayer, Pos pos)
    {
        var d = isPlayer ? playerPositionMap : rivalPositionMap;
        return d.Values.FindFirst(position => position.Pos == pos);
    }

    public ICardInfo GetCardFromId(int id)
    {
        return GetCardFromId(id, true) ?? GetCardFromId(id, false);
    }

    public ICardInfo GetCardFromId(int id, bool isPlayer)
    {
        ICardInfo cardInfo = null;
        var d = isPlayer ? playerPositionMap : rivalPositionMap;
        foreach (var item in d)
        {
            cardInfo = item.Value.Cards.FindFirst(c => c.GameId == id);
            if (cardInfo != null) break;
        }
        return cardInfo;
    }

    public List<ICardInfo> GetPlayableCards(bool isPlayer, Pos pos)
    {
        List<ICardInfo> cards = new List<ICardInfo>();
        foreach (var item in Resolve(isPlayer,pos).Cards)
        {
            if (item.IsPlayable.Value)
            {
                cards.Add(item);
            }
        }
        return cards;
    }

}