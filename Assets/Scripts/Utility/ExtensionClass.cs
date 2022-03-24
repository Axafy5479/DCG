using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ExtensionClass
{
    public static void ForEach<T>(this IEnumerable<T> sequence,Action<T> action)
    {
        foreach (var item in sequence)
        {
            action(item);
        }
    }


    public static IEnumerable<T> NonNull<T>(this IEnumerable<T> sequence)
    {
        return sequence.Where(element => element != null);
    }

    public static IEnumerable<U> ConvertType<U>(this IEnumerable sequence) where U:class
    {
        List<U> ans = new List<U>();

        foreach (var element in sequence)
        {
            ans.Add(element as U);
        }
        return ans;
    }

    public static IEnumerable<U> ConvertAll<T,U>(this IEnumerable<T> sequence,Func<T,U> func) 
    {
        List<U> ans = new List<U>();
        foreach (var item in sequence)
        {
            ans.Add(func(item));
        }
        return ans;
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence)
    {
        return sequence.OrderBy(i => Guid.NewGuid()).ToList();
    }

    public static int FindIndex<T>(this IEnumerable<T> sequence, T target) where T : class
    {
        for (int i = 0; i < sequence.Count(); i++)
        {
            if(sequence.ElementAt(i) == target)
            {
                return i;
            }
        }
        return -1;
    }

    public static T FindFirst<T>(this IEnumerable<T> sequence,Func<T, bool> func)where T:class
    {
        foreach (var item in sequence)
        {
            if(func(item))return item;
        }
        return null;
    }

    public static T FindMax<T>(this IEnumerable<T> sequence, Func<T,IComparable> func) where T : class
    {
        if (sequence.Count() == 0) return null;
        else if (sequence.Count() == 1) return sequence.ElementAt(0);
        else
        {

            T current = sequence.ElementAt(0);
            IComparable currentVal = func(current); 

            for (int i = 1; i < sequence.Count(); i++)
            {
                if (func(sequence.ElementAt(i)).CompareTo(currentVal) > 0)
                {
                    current = sequence.ElementAt(i);
                    currentVal = func(sequence.ElementAt(i));
                }
            }

            return current;
        }


    }


}
