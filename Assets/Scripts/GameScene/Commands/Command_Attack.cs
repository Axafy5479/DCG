using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Position;
using GameInfo;


namespace Command
{
    public class Command_Attack : CommandBase
    {
        [SerializeField] private int GameId_A;
        [SerializeField] private int GameId_T;

        public Command_Attack(bool isPlayer,int gameId_a,int gameId_t) : base(isPlayer)
        {
            GameId_A = gameId_a;
            GameId_T = gameId_t;    
        }

        protected override void _execute()
        {
            bool condition1 = TurnInfo.I.Turn.Value == IsPlayer;
            var card = PositionLocator.LI.Resolve<Field>(IsPlayer).Cards.FindFirst(c => c.GameId == GameId_A);
            bool condition2 = ((IBattlerInfo)card).AttackNumber.Value>0;

            if (condition1 && condition2)
            {
                Field.Attack(GameId_A, GameId_T,false);
                Result = $"{GameId_A}Ç™{GameId_T}ÇçUåÇ";
            }
            else
            {
                Result = $"{GameId_A}Ç©ÇÁ{GameId_T}ÇÃçUåÇé∏îs";
            }
        }
    }
}
