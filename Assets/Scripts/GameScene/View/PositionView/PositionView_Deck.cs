using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionView_Deck : PositionView
{
    [SerializeField] private int[] initialDeck;
    public int[] InitialDeck => initialDeck;

    public override Pos Pos => Pos.Deck;


}
