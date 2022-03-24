using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState_Deck : CardState_Base
{
    public CardState_Deck(ICardInfo info) : base(info)
    {
    }

    public override Pos Pos => Pos.Deck;
}
