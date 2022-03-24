using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Position
{

    /// <summary>
    /// Positionのインスタンスを取得するためのサービスロケータクラス
    /// </summary>
    public class PositionLocator : PositionLocatorInfo
    {
        protected static PositionLocator instance;
        public static PositionLocator LI => instance ??= new PositionLocator();
        protected PositionLocator() { I = this; }

        /// <summary>
        /// インスタンスとTypeの関係を登録する
        /// </summary>
        /// <param name="isPlayer"></param>
        /// <param name="position"></param>
        public void Register(PositionBase position)
        {
            bool isPlayer = position.IsPlayer;
            var d = (isPlayer ? playerPositionMap : rivalPositionMap);

            if (d.ContainsKey(position.GetType()))
            {
                //二回目以降のプレイの場合は、直前の影響が残っているため更新
                d[position.GetType()] = position;
            }
            else
            {
                // 起動後初めてゲームを開始する場合はAdd
                d.Add(position.GetType(), position);
            }
        }

        /// <summary>
        /// Positionクラスのインスタンスを取得する
        /// </summary>
        /// <typeparam name="T">欲しいインスタンスの型</typeparam>
        /// <param name="isPlayer">プレイヤーのインスタンスか否か</param>
        /// <returns></returns>
        public T Resolve<T>(bool isPlayer) where T:PositionBase
        {
            string s = typeof(T).ToString();
            var p = playerPositionMap;
            return (isPlayer ? playerPositionMap : rivalPositionMap)[typeof(T)] as T;
        }

        /// <summary>
        /// 全てのポジションを返す
        /// </summary>
        /// <returns></returns>
        public List<PositionBase> GetAllPositions(bool isPlayer)
        {
            var d = isPlayer ? playerPositionMap : rivalPositionMap;
            return d.Values.ConvertAll(v=>(PositionBase)v).ToList();
        }

        public void Judge()
        {
            foreach (var item in playerPositionMap.Values)
            {
                ((PositionBase)item).PositionJudge();
            }
            foreach (var item in rivalPositionMap.Values)
            {
                ((PositionBase)item).PositionJudge();
            }
        }

    }
}
