using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class PositionView_Field : PositionView,IDropHandler
{
    public override Pos Pos => Pos.Field;

    /// <summary>
    /// �����̃^�[���ɁA��D�̃J�[�h���h���b�v�����ۂ�
    /// Command_Play�����s
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrop(PointerEventData eventData)
    {
        if(DragCardData.IsDragging && DragCardData.I.PlayType == PlayType.Play)
        {
            if (PositionLocatorInfo.I.Resolve(IsPlayer, Pos.Hand).Cards.Any(c => c.GameId == DragCardData.I.BeginCardId))
            {
                DragCardData.I.SetDropData(0);
            }
        }
    }
}
