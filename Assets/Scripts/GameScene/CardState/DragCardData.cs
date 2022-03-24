using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayType
{
    Play,
    Attack
}

/// <summary>
/// D&Dにより決定した2つの情報(BeginCardId,DropData)を
/// 引数2のメソッド(onDrop)に受け渡すクラス
/// インスタンスは1または0個
/// </summary>
public class DragCardData
{
    private static DragCardData instance;
    public static DragCardData I => instance ?? throw new Exception("ドラッグが開始されていません");
    public static bool IsDragging => instance != null;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="beginCardId">ドラッグ開始により判明するカードのid</param>
    /// <param name="onDrop"> 引数2のメソッド</param>
    internal DragCardData(int beginCardId,Action<int,int> onDrop,PlayType playType)
    {
        if (IsDragging)
        {
            throw new Exception("ドラッグ中です");
        }
        instance = this;

        BeginCardId = beginCardId;
        OnDrop = onDrop;
        PlayType = playType;
    }

    /// <summary>
    /// ドラッグ開始により判明するカードのid
    /// </summary>
    public int BeginCardId { get; }

    /// <summary>
    /// 2変数メソッド
    /// (D&Dは、2変数メソッドの引数を決定するための操作)
    /// </summary>
    private Action<int,int> OnDrop { get; }

    /// <summary>
    /// ドロップ時に判明するデータ
    /// </summary>
    public int? DropData { get; private set; } = null;

    public PlayType PlayType { get; }

    /// <summary>
    /// ドロップ時に判明したデータをセット
    /// </summary>
    /// <param name="data"></param>
    public void SetDropData(int data)
    {
        DropData = data;
    }

    /// <summary>
    /// D&Dが完了したときに呼ぶ
    /// 適切な位置にドロップされている場合は、あらかじめ登録されている2変数メソッドを呼ぶ
    /// ドラッグフラグをfalseにする
    /// </summary>
    internal void OnEndDrag()
    {
        if (DropData is int DropDataInt)
        {
            OnDrop(BeginCardId, DropDataInt);
        }
        instance = null;
    }
}
