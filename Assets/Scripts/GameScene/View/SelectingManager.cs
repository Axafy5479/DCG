using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectingManager
{
    private static SelectingManager instance;
    public static SelectingManager I => instance;
    public bool Selecting => instance != null;

    public HashSet<ICardInfo> Option { get; }
    public ICardInfo SelectedCard { get; set; }
    public SelectingManager(Predicate<ICardInfo> predicate)
    {
        instance = this;
        Option = new HashSet<ICardInfo>();

        foreach (var c in PositionLocatorInfo.I.Resolve(true,Pos.Field).Cards)
        {
            if(predicate(c))Option.Add(c);
        }

        foreach (var c in PositionLocatorInfo.I.Resolve(false, Pos.Field).Cards)
        {
            if (predicate(c)) Option.Add(c);
        }
    }

    public IEnumerator StartSelectng(Action<ICardInfo> afterSelecting)
    {
        if (Option.Count > 0)
        {
            foreach (Transform trn in PosViewLocator.I.Resolve(true, Pos.Field).transform)
            {
                var card = trn.GetComponent<CardVisual>();
                card.SelectButton.Show(Option.Contains(card.CardInfo), card.CardInfo);
            }
            foreach (Transform trn in PosViewLocator.I.Resolve(true, Pos.Hand).transform)
            {
                var card = trn.GetComponent<CardVisual>();
                card.SelectButton.Show(Option.Contains(card.CardInfo), card.CardInfo);
            }
            foreach (Transform trn in PosViewLocator.I.Resolve(false, Pos.Field).transform)
            {
                var card = trn.GetComponent<CardVisual>();
                card.SelectButton.Show(Option.Contains(card.CardInfo), card.CardInfo);
            }

            yield return new WaitWhile(() => SelectedCard == null);

            foreach (Transform trn in PosViewLocator.I.Resolve(true, Pos.Field).transform)
            {
                var card = trn.GetComponent<CardVisual>();
                card.SelectButton.Hide();
            }
            foreach (Transform trn in PosViewLocator.I.Resolve(true, Pos.Hand).transform)
            {
                var card = trn.GetComponent<CardVisual>();
                card.SelectButton.Hide();

            }
            foreach (Transform trn in PosViewLocator.I.Resolve(false, Pos.Field).transform)
            {
                var card = trn.GetComponent<CardVisual>();
                card.SelectButton.Hide();

            }
        }

        afterSelecting(SelectedCard);
        instance = null;
    }
}
