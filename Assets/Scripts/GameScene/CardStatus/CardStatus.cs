using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RX;



[System.Serializable]
public abstract class CardStatus: ICardInfo
{

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="isPlayer">プレイヤーのカードか否か</param>
    public CardStatus(CardBook cardBook,Pos pos,bool isPlayer,int gameId)
    {
        IsPlayer = isPlayer;
        cardName = cardBook.CardName;
        this.pos = new Subject<Pos>(pos);
        this.gameId = gameId;
        CardBook = cardBook;
        cost = new Subject<int>(cardBook.Cost);
        isPlayable = new Subject<bool>(false);
        AbilityId = cardBook.AbilityId;
    }

    [SerializeField] protected Subject<Pos> pos;
    [SerializeField] protected string cardName;
    [SerializeField] protected int gameId;
    [SerializeField] protected Subject<bool> isPlayable;
    [SerializeField] protected Subject<int> cost;


    #region public properties
    public virtual IObservable<Pos> Pos { get => pos; }
    public virtual IObservable<int> Cost { get => cost;  }
    public virtual IObservable<bool> IsPlayable { get => isPlayable; }

    /// <summary>
    /// カードのDB
    /// </summary>
    public CardBook CardBook { get; }

    /// <summary>
    /// カードの種類(キャラカードか、スペルカードか、etc)
    /// </summary>
    public abstract CardType Type { get; }

    public int GameId => gameId;

    public int AbilityId { get; }


    /// <summary>
    /// プレイヤーのカードか否か
    /// </summary>
    public bool IsPlayer { get; }

    public AbilityBook AbilityBook => AbilityLocatorInfo.I.Resolve(AbilityId);

    public int PositionIndex => PositionLocatorInfo.I.Resolve(IsPlayer, Pos.Value).Cards.FindIndex(this);
    #endregion

    #region public Methods
    /// <summary>
    /// Debug.Log の時にCardNameを表示させる
    /// </summary>
    /// <returns></returns>
    public override string ToString() => CardBook.CardName;

    /// <summary>
    /// カードのポジションを変更する
    /// </summary>
    /// <param name="posTo"></param>
    /// <returns></returns>
    public void ChangePos(Pos posTo) => pos.OnNext(posTo);


    public void ChangePlayable(bool canPlay)
    {
        isPlayable.OnNext(canPlay);
    }


    #endregion

}
