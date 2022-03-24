using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RX;

namespace GameInfo
{
    /// <summary>
    /// 現在どちらのターンであるかの情報を保持したクラス。
    /// TurnManager  --|>  TurnInfo
    /// の関係となっており、TurnManagerはシングルトン。
    /// TurnManagerのインスタンスが生成されたタイミングで TurnInfo.I に値が入る
    /// </summary>
    public abstract class TurnInfo
    {
        /// <summary>
        /// 継承先であるTurnManagerのインスタンスが生成されたタイミングで
        /// インスタンスが代入される
        /// </summary>
        private static TurnInfo instance;

        /// <summary>
        /// TurnInfoのインスタンス
        /// </summary>
        public static TurnInfo I 
        {
            get
            {
                if (instance == null)
                {
                    //TurnManagerのインスタンスを生成するまでは、
                    //instanceはnullとなっている
                    throw new System.Exception("先にTurnManagerのインスタンスを生成してください");
                }
                return instance;
            } 
            protected set => instance = value; 
        }

        /// <summary>
        /// 現在どちらのターンか
        /// </summary>
        public IObservable<bool> Turn { get; protected set; }
    }
}
