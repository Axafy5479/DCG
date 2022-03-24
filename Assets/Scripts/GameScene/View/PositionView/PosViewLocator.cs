using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosViewLocator
{
    private static PosViewLocator instance;
    public static PosViewLocator I => instance ??= new PosViewLocator();
    private PosViewLocator() { }



    /// <summary>
    /// プレイヤーのPositionクラス
    /// </summary>
    private Dictionary<Type, PositionView> playerPositionMap = new Dictionary<Type, PositionView>();

    /// <summary>
    /// 対戦相手のPositionクラス
    /// </summary>
    private Dictionary<Type, PositionView> rivalPositionMap = new Dictionary<Type, PositionView>();

    /// <summary>
    /// インスタンスとTypeの関係を登録する
    /// </summary>
    /// <param name="isPlayer"></param>
    /// <param name="position"></param>
    public void Register(PositionView position)
    {
        bool isPlayer = position.IsPlayer;

        var d = (isPlayer ? playerPositionMap : rivalPositionMap);

        if (d.ContainsKey(position.GetType()))
        {
            //二回目以降のプレイの場合は、直前の影響が残っているため更新
            d[position.GetType()] = position;
        }
        else
        {
            // 起動後初めてゲームを開始する場合はAdd
            d.Add(position.GetType(), position);
        }
    }


    /// <summary>
    /// Positionクラスのインスタンスを取得する
    /// </summary>
    /// <typeparam name="T">欲しいインスタンスの型</typeparam>
    /// <param name="isPlayer">プレイヤーのインスタンスか否か</param>
    /// <returns></returns>
    public T Resolve<T>(bool isPlayer) where T : PositionView
    {
        return (isPlayer ? playerPositionMap : rivalPositionMap)[typeof(T)] as T;
    }

    public PositionView Resolve(bool isPlayer, Pos pos)
    {
        var d = isPlayer ? playerPositionMap : rivalPositionMap;

        foreach (var item in d)
        {
            if (item.Value.Pos == pos) return item.Value;
        }
        return null;
    }

}
