using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RX
{

    /// <summary>
    /// 自前UniRx
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Subject<T> : RX.IObserver<T>, RX.IObservable<T>
    {
        //現在の値
        [SerializeField] private T value;


        //購読しているDisposable
        private List<Disposable> Actions { get; }
        public T Value { get => value; private set => this.value = value; }

        //コンストラクタ
        public Subject(T val)
        {
            Actions = new List<Disposable>();
            Value = val;
        }

        /// <summary>
        /// 値の変更かつ通知
        /// </summary>
        /// <param name="val"></param>
        public void OnNext(T val)
        {
            Value = val;
            foreach (var a in Actions)
            {
                a.A(val);
            }
        }

        /// <summary>
        /// 購読
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public IDisposable Subscribe(Action<T> a)
        {
            var d = new Disposable(a, Actions);
            Actions.Add(d);
            return d;
        }

        private class Disposable : IDisposable
        {
            public Action<T> A { get; }
            List<Disposable> L { get; }
            public Disposable(Action<T> a, List<Disposable> l)
            {
                A = a;
                L = l;
            }

            public void Dispose()
            {
                L.Remove(this);
            }
        }
    }


}
