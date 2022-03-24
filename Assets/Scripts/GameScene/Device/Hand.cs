using RX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// ��D�̋@�\����������N���X
    /// </summary>
    public class Hand : PositionBase, IHandPosition
    {

        public Hand(bool isPlayer) : base(isPlayer)
        {
            //�}�i�N���X�̏�����
            ManaManager = Settings.I.DebugMode ?

                //�f�o�b�O���[�h�̏ꍇ
                new Mana_Debug()

                //�f�o�b�O���[�h�ł͂Ȃ��ꍇ
                : new Mana();
        }

        public override Pos Pos => Pos.Hand;

        /// <summary>
        /// �}�i�N���X
        /// </summary>
        public Mana ManaManager { get; private set; }

        /// <summary>
        /// ��D�̖����̏����7��
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
