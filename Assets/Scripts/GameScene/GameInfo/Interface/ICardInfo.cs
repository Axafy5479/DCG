using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RX;

/// <summary>
/// 全てのカードがもつ変数のうち、読み取り専用部分のみを抜き出したインターフェース
/// </summary>
public interface ICardInfo
{
    /// <summary>
    /// カードの名前
    /// </summary>
    CardBook CardBook { get; }

    /// <summary>
    /// カードが配置されている位置
    /// </summary>
    IObservable<Pos> Pos { get; }

    /// <summary>
    /// 現在プレイ可能か否か (手札のカードでも使用するため、IPlayable以外でも必要になる性質)
    /// </summary>
    IObservable<bool> IsPlayable { get; }

    /// <summary>
    /// プレイヤーのカードか否か
    /// </summary>
    bool IsPlayer { get; }

    /// <summary>
    /// このカードの種類
    /// </summary>
    CardType Type { get; }

    /// <summary>
    /// ゲーム中のカードを特定するID
    /// </summary>
    int GameId { get; }

    /// <summary>
    /// プレイ時Ability
    /// </summary>
    AbilityBook AbilityBook { get; }


    /// <summary>
    /// プレイする際に消費するコスト
    /// </summary>
    /// <returns></returns>
    IObservable<int> Cost { get; }

    /// <summary>
    /// 現在Posの何番目に配置されているか
    /// </summary>
    int PositionIndex { get; }
}
