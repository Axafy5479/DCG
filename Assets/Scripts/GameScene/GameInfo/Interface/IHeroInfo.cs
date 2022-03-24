using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RX;

public interface IHeroInfo
{
    public IObservable<int> Hp { get; }
    public IObservable<int> Atk { get; }
    public bool IsPlayer { get; }
}
