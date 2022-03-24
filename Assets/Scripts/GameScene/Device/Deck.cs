using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// デッキの機能を実装するクラス
    /// </summary>
    public class Deck : PositionBase
    {
        //初期のデッキ(BookIdで表現)
        [SerializeField] private CardBook[] initialDeck_Debug;

        public Deck(bool isPlayer) : base(isPlayer)
        {
        }

        public void MakeInitialDeck(CardBook[] initialDeck)
        {
            initialDeck_Debug = initialDeck;

            //BookIdで表現された初期のデッキをもとにカードを生成
            initialDeck_Debug.ForEach(b => MakeCardAndAdd(b,0));
        }

        public override Pos Pos => Pos.Deck;

        /// <summary>
        /// デッキのカードの上限はないため、常にtrue
        /// (初期デッキの枚数は指定するが、ゲーム中に行えるデッキ補充効果に枚数制限はない)
        /// </summary>
        /// <returns></returns>
        public override bool CanAdd() => true;

        /// <summary>
        /// カードを手札に移動
        /// </summary>
        /// <returns></returns>
        public ICardInfo Draw()
        {
            if(Cards.Count==0) return null;

            //Handクラスのインスタンスを見つける
            Hand hand =PositionLocator.LI.Resolve<Hand>(IsPlayer);
            var drawingCard = Cards[0];

            //手札の最後尾に、デッキの先頭のカードを移動
            TryMoveTo<Hand>(hand.Cards.Count,drawingCard.GameId);

            return drawingCard;
        }

        protected override void _onBeginTurn()
        {
            Draw();
        }
    }
}
