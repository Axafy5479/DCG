using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RX
{

    /// <summary>
    /// ���OUniRx
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Subject<T> : RX.IObserver<T>, RX.IObservable<T>
    {
        //���݂̒l
        [SerializeField] private T value;


        //�w�ǂ��Ă���Disposable
        private List<Disposable> Actions { get; }
        public T Value { get => value; private set => this.value = value; }

        //�R���X�g���N�^
        public Subject(T val)
        {
            Actions = new List<Disposable>();
            Value = val;
        }

        /// <summary>
        /// �l�̕ύX���ʒm
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
        /// �w��
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
