using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RX
{
    public interface IObservable<T>
    {
        System.IDisposable Subscribe(System.Action<T> observer);
        public T Value { get; }
    }
}
