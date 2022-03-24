using Position;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInfo;


namespace Command
{
    /// <summary>
    /// カードをプレイするコマンド
    /// </summary>
    public class Command_Play : CommandBase
    {
        /// <summary>
        /// プレイしたいカードのGameId
        /// </summary>
        [SerializeField] private int gameId;

        /// <summary>
        /// プレイ位置
        /// </summary>
        [SerializeField] private int indexTo;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="isPlayer">プレイヤーのコマンドか否か</param>
        /// <param name="gameId">プレイするカードのGameId</param>
        /// <param name="indexTo">プレイ先の位置</param>
        public Command_Play(bool isPlayer,int gameId,int indexTo):base(isPlayer)
        {
            this.gameId = gameId;
            this.indexTo = indexTo;
        }

        /// <summary>
        /// 実際にカードをプレイする
        /// </summary>
        protected override void _execute()
        {
            //Handクラスのインスタンスを解決
            Hand h = PositionLocator.LI.Resolve<Hand>(IsPlayer);

            //GameIdからカードのインターフェースを取得
            ICardInfo playingCard = h.Cards.FindFirst(c=>c.GameId==gameId);

            //自身のターンか否か
            bool condition1 = TurnInfo.I.Turn.Value == IsPlayer;

            //マナが十分にあるか
            bool condition2 = h.Mana.Value >= playingCard.Cost.Value;

            //場面にカードを配置できるか
            bool condition3 = PositionLocator.LI.Resolve<Field>(IsPlayer).CanAdd();

            if (!condition1 || !condition2 || !condition3)
            {
                Result = $"{(IsPlayer ? "プレイヤー" : "対戦相手")}: {playingCard}のプレイに失敗";
                return;
            }
            else
            {
                //プレイに要するマナを所持しているか
                if (h.ManaManager.TryUseMana(playingCard.Cost.Value))
                {
                    //所持している場合はカードのポジションを変更
                    if(h.TryMoveTo<Field>(indexTo, gameId))
                    {
                        RunAbility_OnPlay(playingCard);
                    }
                }

                Result = $"{(IsPlayer ? "プレイヤー" : "対戦相手")}: {playingCard}をプレイ";
            }
        }

        protected virtual void RunAbility_OnPlay(ICardInfo playingCard)
        {
            if (!(playingCard.AbilityBook is AbilityBook_Selectable))
            {
                ((IAbilityBase)playingCard.AbilityBook.AbilityGetter()).Run(playingCard);
            }
        }
    }
}
