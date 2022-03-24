using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbilityBase : IAbilityInfo
{
    public abstract void Run(ICardInfo owner);
}
