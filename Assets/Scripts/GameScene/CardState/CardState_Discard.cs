using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardState_Discard : CardState_Base
{
    public CardState_Discard(ICardInfo info) : base(info)
    {
    }
    public override Pos Pos => Pos.Discard;

}
