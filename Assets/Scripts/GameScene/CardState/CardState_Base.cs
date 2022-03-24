using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardState_Base
{

    public CardState_Base(ICardInfo info)
    {
        CardInfo = info;
    }

    public ICardInfo CardInfo { get; }
    public bool IsPlayer => CardInfo.IsPlayer;
    public abstract Pos Pos { get; }

    public virtual void OnClick() { }
    public virtual bool OnBeginDrag() { return false; }
    public virtual bool OnDrag() { return false; }
    public virtual bool OnDrop() { return false; }
    public virtual bool OnEndDrag() { return false; }

}
