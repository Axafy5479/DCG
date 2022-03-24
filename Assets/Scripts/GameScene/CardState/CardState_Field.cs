using Command;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardState_Field : CardState_Base
{
    public CardState_Field(ICardInfo info) : base(info) { }
    public override Pos Pos => Pos.Field;

    public override bool OnBeginDrag()
    {
        //���ɑ��̈ʒu�Ńh���b�O���J�n���Ă��邩�ۂ����m�F
        if (DragCardData.IsDragging)
        {
            //2�����Ƀh���b�O���邱�Ƃ͋֎~���Ă���
            return false;
        }
        else
        {
            if (CardInfo.IsPlayable.Value)
            {
                new DragCardData(CardInfo.GameId, OnDecideTarget, PlayType.Attack);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public override bool OnEndDrag()
    {
        bool condition1 = !DragCardData.IsDragging;
        bool condition2 = DragCardData.I.DropData == null;

        //Drag���J�n����Ă��邩�A�����ăh���b�v�悪�������Ă��邩
        if (condition1 || condition2)
        {
            return false;
        }
        else
        {
            DragCardData.I.OnEndDrag();
            return true;
        }
    }

    public override bool OnDrop()
    {
        if (DragCardData.IsDragging && DragCardData.I.PlayType == PlayType.Attack)
        {
            var attacker_is_rival = PositionLocatorInfo.I.Resolve(!IsPlayer, Pos.Field).Cards.Any(c => c.GameId == DragCardData.I.BeginCardId);

            if(attacker_is_rival)
            {
                DragCardData.I.SetDropData(CardInfo.GameId);
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    private void OnDecideTarget(int attackerId, int targetId)
    {
        if (targetId >= 0)
        {
            CommandInvoker.I.Invoke(new Command_Attack(IsPlayer, attackerId, targetId));
        }
        else if(targetId == -1)
        {
            CommandInvoker.I.Invoke(new Command_AttackHero(IsPlayer, attackerId));
        }
    }
}
