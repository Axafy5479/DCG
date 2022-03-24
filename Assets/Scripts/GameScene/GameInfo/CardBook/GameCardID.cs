using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameIdを生成するクラス
/// 全てのカードのインスタンスIDとして使用
/// (オンライン対戦の際、同じカードには同じIDが付与される)
/// </summary>
public class GameCardID:MonoSingleton<GameCardID>
{
    /// <summary>
    /// GameIdとBookIdを紐づける
    /// </summary>
    private Dictionary<int, int> GameId_BookId_Map = new Dictionary<int, int>();

    /// <summary>
    /// 通信対戦相手に、自身のGameIdと矛盾せずにIdを渡すためのQueue
    /// </summary>
    private Queue<(int, int)> GameId_BookId_Queue = new Queue<(int,int)>();

    /// <summary>
    /// 次にGetIdが呼ばれた際に渡すId
    /// 1つ返すたびにインクリメント
    /// </summary>
    private int NextId
    {
        get
        {
            int id = nextId;
            nextId++;
            return id;
        }
    }
    private int nextId = 1;


    /// <summary>
    /// GameIdを発行する
    /// </summary>
    /// <param name="bookId"></param>
    /// <returns></returns>
    public int GetId(int bookId)
    {
        int id = NextId;

        //GameIdとBoookIdの関係を追加
        GameId_BookId_Map.Add(id, bookId);

        //Queueにも同様のペアを追加
        GameId_BookId_Queue.Enqueue((id, bookId));

        return id;
    }


}
