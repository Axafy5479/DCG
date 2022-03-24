using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class CardBook_Chara : CardBook
{
    [SerializeField]private int atk, hp;

    protected CardBook_Chara(int bookId, string cardName, string name_en, string description, int cost, int hp, int atk, int abilityId) : base(bookId, cardName, name_en, description, cost, abilityId)
    {
        this.hp = hp;
        this.atk = atk;
    }

    /// <summary>
    /// 攻撃力
    /// </summary>
    public int Atk { get => atk; }

    /// <summary>
    /// 初期体力
    /// </summary>
    public int Hp { get => hp; }

    /// <summary>
    /// カードの種類(=キャラカード)
    /// </summary>
    public override CardType CardType => CardType.Chara;

}

