using RX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandPosition
{
    public IObservable<int> Mana { get; }
}
