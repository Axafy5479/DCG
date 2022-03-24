using RX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// 手札の機能を実装するクラス
    /// </summary>
    public class Hand : PositionBase, IHandPosition
    {

        public Hand(bool isPlayer) : base(isPlayer)
        {
            //マナクラスの初期化
            ManaManager = Settings.I.DebugMode ?

                //デバッグモードの場合
                new Mana_Debug()

                //デバッグモードではない場合
                : new Mana();
        }

        public override Pos Pos => Pos.Hand;

        /// <summary>
        /// マナクラス
        /// </summary>
        public Mana ManaManager { get; private set; }

        /// <summary>
        /// 手札の枚数の上限は7枚
        /// </summary>
        /// <returns></returns>
        public override bool CanAdd() => Cards.Count < 7;

        protected override void _onBeginTurn()
        {
            ManaManager.NewTurn();
        }

        public override void PositionJudge()
        {
            foreach (var c in cards)
            {
                c.ChangePlayable(c.Cost.Value <= ManaManager.CurrentMana.Value && IsPlayer == TurnManager.I.Turn.Value);
            }
        }

        public IObservable<int> Mana => ManaManager.CurrentMana;
    }

   
}
