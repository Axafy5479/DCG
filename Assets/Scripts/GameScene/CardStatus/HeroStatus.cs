using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RX;

public class HeroStatus: IHeroInfo
{
    public HeroStatus(bool isPlayer, int initialHp,int initialAtk = 0)
    {
        IsPlayer = isPlayer;
        hp = new Subject<int>(initialHp);
        atk = new Subject<int>(initialAtk);
        HeroLocator.HI.Register(this);
    }

    private Subject<int> hp;
    private Subject<int> atk;

    public bool IsPlayer { get; }
    public IObservable<int> Hp => hp;
    public IObservable<int> Atk => atk;

    public int Damage(int damage)
    {
        int prevHp = hp.Value;
        int temp = hp.Value - damage;
        if(temp < 0)
        {
            hp.OnNext(0);
        }
        else
        {
            hp.OnNext(temp);
        }
        return prevHp-hp.Value;
    }
}
