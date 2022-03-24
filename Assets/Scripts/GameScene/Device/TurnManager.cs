using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInfo;
using RX;
using Position;

public class TurnManager : TurnInfo
{
    private static TurnManager instance;
    public static TurnManager TI => instance;
    private IObserver<bool> turn { get; }
    private TurnManager(bool firsutAttack)
    {

        FirstAttack = firsutAttack;
        turn = new Subject<bool>(firsutAttack);
        Turn = (IObservable<bool>)turn;
        I = this;
    }


    public static void Initialize(bool firsutAttack)
    {
        instance = null;
        instance = new TurnManager(firsutAttack);
    }

    public void GameStart()
    {
        PositionLocator.LI.Resolve<Hand>(FirstAttack).OnBeginTurn(FirstAttack);
        PositionLocator.LI.Judge();
    }

    public bool FirstAttack { get; private set; }
    public bool ChangeTurn()
    {
        turn.OnNext(!Turn.Value);
        return Turn.Value;
    }
}
