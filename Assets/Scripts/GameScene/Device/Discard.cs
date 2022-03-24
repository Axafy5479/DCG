using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// 捨て札の機能を実装するクラス
    /// </summary>
    public class Discard : PositionBase
    {
        public Discard(bool isPlayer) : base(isPlayer)
        {
        }

        public override Pos Pos => Pos.Discard;

        /// <summary>
        /// 捨て札には何枚でもカードを追加可能
        /// </summary>
        /// <returns></returns>
        public override bool CanAdd() => true;

    }
}
