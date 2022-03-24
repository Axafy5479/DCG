using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardDataBaseInfo
{
    public static CardDataBaseInfo I { get; protected set; }

    protected Dictionary<int, CardBook> CardBook { get; set; }


    /// <summary>
    /// ヒーローのインスタンスを取得する
    /// </summary>
    /// <param name="isPlayer">プレイヤーor対戦相手</param>
    /// <returns>ヒーローのインスタンス</returns>
    public CardBook GetBook(int id) => CardBook[id];
}
