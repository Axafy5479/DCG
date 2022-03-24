using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController
{
    public HeroController(bool isPlayer,int initialHp)
    {
        IsPlayer = isPlayer;
        Hero = new HeroStatus(isPlayer, initialHp);
    }

    public bool IsPlayer { get; }
    private HeroStatus Hero { get; }


}
