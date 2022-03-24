using GameInfo;
using Position;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Command
{
    public class Command_AttackHero : CommandBase
    {
        [SerializeField] private int GameId_A;

        public Command_AttackHero(bool isPlayer,int attacker) : base(isPlayer)
        {
            GameId_A = attacker;
        }

        protected override void _execute()
        {
            bool condition1 = TurnInfo.I.Turn.Value == IsPlayer;
            var card = PositionLocator.LI.Resolve<Field>(IsPlayer).Cards.FindFirst(c => c.GameId == GameId_A);
            bool condition2 = ((IBattlerInfo)card).AttackNumber.Value > 0;

            if (condition1 && condition2)
            {
                Field.AttackHero(GameId_A,false);
                Result = $"{GameId_A}‚ªƒq[ƒ[‚ğUŒ‚";
            }
            else
            {
                Result = $"{GameId_A}‚©‚çƒq[ƒ[‚ÌUŒ‚¸”s";
            }
        }
    }
}
