using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターカードが実装するインターフェース
/// </summary>
public interface IChangeStatus: IBattlerInfo
{
    /// <summary>
    /// 死亡時に呼ばれるメソッド
    /// </summary>
    public void OnDead();

    /// <summary>
    /// Hpを減少させるメソッド
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int Damage(int damage);

    /// <summary>
    /// 状態異常の追加
    /// </summary>
    /// <param name="statusEffect"></param>
    public void AddStatusEffect(StatusEffect statusEffect);

    /// <summary>
    /// 状態異常の除去
    /// </summary>
    /// <param name="statusEffect"></param>
    public void RemoveStatusEffect(StatusEffect statusEffect);
}
