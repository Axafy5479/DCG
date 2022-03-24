using Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState_Hand : CardState_Base
{
    private Action<Predicate<ICardInfo>,Action<ICardInfo>> selectManager { get; }
    public CardState_Hand(ICardInfo info, Action<Predicate<ICardInfo>, Action<ICardInfo>> selectManager) : base(info)
    {
        this.selectManager = selectManager;
    }
    public override Pos Pos => Pos.Hand;

    private DragCardData _DragCardData { get; set; }

    public override bool OnBeginDrag()
    {
        //既に他の位置でドラッグが開始しているか否かを確認
        if (DragCardData.IsDragging)
        {
            //2つ同時にドラッグすることは禁止している
            return false;
        }
        else
        {
            if (CardInfo.IsPlayable.Value)
            {
                _DragCardData = new DragCardData(CardInfo.GameId, OnDecidePlayPosition, PlayType.Play);
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

        //Dragが開始されているか、そしてドロップ先が判明しているか
        if ( condition1||condition2 )
        {
            //2つ同時にドラッグすることは禁止している
            return false;
        }
        else
        {
            DragCardData.I.OnEndDrag();
            return true;
        }
    }

    private void OnDecidePlayPosition(int playingCardId,int pos)
    {
        var c = PositionLocatorInfo.I.GetCardFromId(playingCardId);
        if (c.AbilityBook.Selectable)
        {
            Action<ICardInfo> afterSelecting = selected=> RunAbility_with_SelectedCard(playingCardId, pos, selected);

            selectManager(((AbilityBook_Selectable)c.AbilityBook).Predicate, afterSelecting);

            
        }
        else
        {
            CommandInvoker.I.Invoke(new Command_Play(IsPlayer, playingCardId, pos));
        }
    }

    private void RunAbility_with_SelectedCard(int playingCardId,int pos,ICardInfo selected)
    {
        if (selected == null)
        {
            CommandInvoker.I.Invoke(new Command_Play(IsPlayer, playingCardId, pos));
        }
        else
        {
            CommandInvoker.I.Invoke(new Command_Play_WithSelecting(IsPlayer, playingCardId, pos, selected.GameId));
        }
    }


}



