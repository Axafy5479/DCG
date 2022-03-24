using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class CardDataBase : CardDataBaseInfo
{
    internal CardDataBase(List<CardBook> books)
    {
        I = this;
        CardBook = new Dictionary<int, CardBook>();

        foreach (var b in books)
        {
            CardBook.Add(b.BookId,b);
        }
    }
}
