using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// �f�b�L�̋@�\����������N���X
    /// </summary>
    public class Deck : PositionBase
    {
        //�����̃f�b�L(BookId�ŕ\��)
        [SerializeField] private CardBook[] initialDeck_Debug;

        public Deck(bool isPlayer) : base(isPlayer)
        {
        }

        public void MakeInitialDeck(CardBook[] initialDeck)
        {
            initialDeck_Debug = initialDeck;

            //BookId�ŕ\�����ꂽ�����̃f�b�L�����ƂɃJ�[�h�𐶐�
            initialDeck_Debug.ForEach(b => MakeCardAndAdd(b,0));
        }

        public override Pos Pos => Pos.Deck;

        /// <summary>
        /// �f�b�L�̃J�[�h�̏���͂Ȃ����߁A���true
        /// (�����f�b�L�̖����͎w�肷�邪�A�Q�[�����ɍs����f�b�L��[���ʂɖ��������͂Ȃ�)
        /// </summary>
        /// <returns></returns>
        public override bool CanAdd() => true;

        /// <summary>
        /// �J�[�h����D�Ɉړ�
        /// </summary>
        /// <returns></returns>
        public ICardInfo Draw()
        {
            if(Cards.Count==0) return null;

            //Hand�N���X�̃C���X�^���X��������
            Hand hand =PositionLocator.LI.Resolve<Hand>(IsPlayer);
            var drawingCard = Cards[0];

            //��D�̍Ō���ɁA�f�b�L�̐擪�̃J�[�h���ړ�
            TryMoveTo<Hand>(hand.Cards.Count,drawingCard.GameId);

            return drawingCard;
        }

        protected override void _onBeginTurn()
        {
            Draw();
        }
    }
}
