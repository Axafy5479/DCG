using RX;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// すべてのポジションはこの抽象クラスを継承する
    /// </summary>
    public abstract class PositionBase : IPosition
    {
        public PositionBase(bool isPlayer)
        {
            IsPlayer = isPlayer;
        }

        protected Subject<ICardInfo> cardMade  = new Subject<ICardInfo>(null);
        public IObservable<ICardInfo> CardMade => cardMade;

        /// <summary>
        /// プレイヤーのPositionか否か
        /// </summary>
        public bool IsPlayer { get; }

        /// <summary>
        /// このポジションに存在するカード全て
        /// </summary>
        public ReadOnlyCollection<ICardInfo> Cards =>_cards.ConvertAll(c=>c as ICardInfo).AsReadOnly();
        protected ReadOnlyCollection<ICardMover> cards => _cards.AsReadOnly();
        private List<ICardMover> _cards = new List<ICardMover>();

        public abstract Pos Pos { get; }

        /// <summary>
        /// カードを移動する
        /// </summary>
        /// <typeparam name="T">移動先</typeparam>
        /// <param name="index">移動先での配置位置</param>
        /// <param name="gameid">移動するカードのId</param>
        /// <returns></returns>
        public bool TryMoveTo<T>(int index,int gameid)where T:PositionBase
        {
            //gameidからカードのインターフェースを探す
            ICardMover card = _cards.Find(c=>c.GameId == gameid);

            //型「T」から移動先のインスタンスを探す
            PositionBase positionTo = PositionLocator.LI.Resolve<T>(IsPlayer);

            //移動先にカードが追加できる状態なら...
            if(positionTo.CanAdd())
            {
                //カードを除去出来たならば...
                if(TryRemove(card))
                {
                    //カードを追加
                    positionTo.Add(index,card);

                    //カードのStatusのPosを変更
                    card.ChangePos(positionTo.Pos);

                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// カードを追加できる状態か否か
        /// </summary>
        /// <returns></returns>
        public abstract bool CanAdd();

        /// <summary>
        /// カードを追加する
        /// </summary>
        /// <param name="index"></param>
        /// <param name="card"></param>
        protected virtual void Add(int index, ICardInfo card)
        {
            _cards.Insert(index,card as ICardMover);
        }

        /// <summary>
        /// カードを除去する
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        protected virtual bool TryRemove(ICardInfo card)
        {
            return _cards.Remove(card as ICardMover);
        }

        public ICardInfo MakeCardAndAdd(CardBook book,int index)
        {
            ICardInfo cardInfo = book switch
            {
                CardBook_Chara chara => new CardStatus_Chara(chara, Pos, IsPlayer, GameCardID.I.GetId(book.BookId)),
                _=>throw new System.Exception(book.GetType()+"に相当するステータスが存在しません")
            };

            Add(index, cardInfo);

            cardMade.OnNext(cardInfo);

            //PlayableIdを設定
            return cardInfo;
        }

        public void OnBeginTurn(bool turn)
        {
            if (turn != IsPlayer)
            {
                foreach (var c in cards)
                {
                    ((CardStatus)c).ChangePlayable(false);
                }
            }
            else
            {
                _onBeginTurn();
            }
        }

        protected virtual void _onBeginTurn() { }
        public virtual void PositionJudge() { }
    }
}
