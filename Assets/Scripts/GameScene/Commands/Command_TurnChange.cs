using Position;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInfo;

namespace Command
{
    public class Command_TurnChange : CommandBase
    {
        public Command_TurnChange(bool isPlayer) : base(isPlayer)
        {
            NextTurn = !TurnInfo.I.Turn.Value;
        }

        public bool NextTurn { get; }

        protected override void _execute()
        {
            TurnManager.TI.ChangeTurn();
            PositionLocator.LI.GetAllPositions(!NextTurn).ForEach(p => p.OnBeginTurn(NextTurn));
            PositionLocator.LI.GetAllPositions(NextTurn).ForEach(p => p.OnBeginTurn(NextTurn));

        }
    }
}
