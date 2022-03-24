using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using RX;

/// <summary>
/// キャラクターカードが持つ変数のうち、読み取り専用部分のみ抜き出したインターフェース
/// </summary>
public interface IBattlerInfo: ICardInfo
{
    /// <summary>
    /// 残りの攻撃可能回数
    /// </summary>
    public IObservable<int> AttackNumber { get; }

    /// <summary>
    /// Hpの値
    /// </summary>
    public IObservable<int> Hp { get; }//=> HpData;

    /// <summary>
    /// Atkの値
    /// </summary>
    public IObservable<int> Atk { get; }//=> HpData;

    /// <summary>
    /// かかっている状態異常
    /// </summary>
    public ReadOnlyCollection<StatusEffect> StatusEffects { get; }
}
