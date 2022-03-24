using RX;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/// <summary>
/// カードを配置できる位置
/// </summary>
public enum Pos
{
    Deck,
    Hand,
    Field,
    Discard,
    Hero
}

/// <summary>
/// PositionBaseの変数のうち、
/// 読み取り専用な変数のみ公開するインターフェース
/// </summary>
public interface IPosition
{
    /// <summary>
    /// 存在するカード
    /// </summary>
    ReadOnlyCollection<ICardInfo> Cards { get; }

    /// <summary>
    /// プレイヤーor対戦相手
    /// </summary>
    bool IsPlayer { get; }

    /// <summary>
    /// どのポジションか
    /// </summary>
    Pos Pos { get; }

    IObservable<ICardInfo> CardMade { get; }
}

