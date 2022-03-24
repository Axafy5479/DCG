using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// フィールドの機能を実装するクラス
    /// </summary>
    public class Field : PositionBase
    {
        public Field(bool isPlayer) : base(isPlayer)
        {
        }

        public override Pos Pos => Pos.Field;

        /// <summary>
        /// フィールドの枚数は5枚まで
        /// </summary>
        /// <returns></returns>
        public override bool CanAdd()=> Cards.Count < 5;

        public static void Attack(int gameId_a, int gameId_t,bool specialAttack)
        {
            AttackManager.AttackCard(gameId_a, gameId_t, specialAttack);
        }

        public static void AttackHero(int gameId_a,bool specialAttack)
        {
            AttackManager.AttackHero(gameId_a, specialAttack);
        }

        protected override void _onBeginTurn()
        {
            foreach (var c in Cards)
            {
                if (c is CardStatus_Chara chara)
                {
                    chara.ResetAttackNum();
                }
            }
        }

        public override void PositionJudge()
        {
            foreach (var c in new List<ICardMover>(cards))
            {
                if(c is CardStatus_Chara chara)
                {
                    if (chara.StatusEffects.Contains(StatusEffect.Dead))
                    {
                        TryMoveTo<Discard>(0,chara.GameId);
                    }
                    else
                    {
                        chara.ChangePlayable(chara.AttackNumber.Value > 0 && IsPlayer == TurnManager.I.Turn.Value);
                    }

                }

            }
        }
    }
}
