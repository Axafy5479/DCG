using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityLocatorInfo
{
    private static AbilityLocatorInfo instance;
    public static AbilityLocatorInfo I=>instance??=new AbilityLocatorInfo();

    private AbilityLocatorInfo() { }

    private Dictionary<int, AbilityBook> locations = new Dictionary<int, AbilityBook>();

    public void Register(AbilityBook book)
    {
        locations.Add(book.Id, book);
    }

    public AbilityBook Resolve(int id) => locations[id];

    public static void Reflesh()
    {
        instance = null;
    }

}


public class AbilityBook
{
    public AbilityBook(Func<IAbilityInfo> abilityGetter, bool selectable,int id)
    {
        AbilityGetter = abilityGetter;
        Selectable = selectable;
        Id = id;
    }

    public Func<IAbilityInfo> AbilityGetter { get; }
    public bool Selectable { get; }
    public int Id { get; }
}


public class AbilityBook_Selectable : AbilityBook
{
    public AbilityBook_Selectable(Func<IAbilityInfo> abilityGetter, bool selectable, int id, Predicate<ICardInfo> predicate) : base(abilityGetter, selectable, id)
    {
        Predicate = predicate;

    }
    public Predicate<ICardInfo> Predicate { get; }

}