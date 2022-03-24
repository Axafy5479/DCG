using Position;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInfo;


namespace Command
{
    /// <summary>
    /// �J�[�h���v���C����R�}���h
    /// </summary>
    public class Command_Play : CommandBase
    {
        /// <summary>
        /// �v���C�������J�[�h��GameId
        /// </summary>
        [SerializeField] private int gameId;

        /// <summary>
        /// �v���C�ʒu
        /// </summary>
        [SerializeField] private int indexTo;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="isPlayer">�v���C���[�̃R�}���h���ۂ�</param>
        /// <param name="gameId">�v���C����J�[�h��GameId</param>
        /// <param name="indexTo">�v���C��̈ʒu</param>
        public Command_Play(bool isPlayer,int gameId,int indexTo):base(isPlayer)
        {
            this.gameId = gameId;
            this.indexTo = indexTo;
        }

        /// <summary>
        /// ���ۂɃJ�[�h���v���C����
        /// </summary>
        protected override void _execute()
        {
            //Hand�N���X�̃C���X�^���X������
            Hand h = PositionLocator.LI.Resolve<Hand>(IsPlayer);

            //GameId����J�[�h�̃C���^�[�t�F�[�X���擾
            ICardInfo playingCard = h.Cards.FindFirst(c=>c.GameId==gameId);

            //���g�̃^�[�����ۂ�
            bool condition1 = TurnInfo.I.Turn.Value == IsPlayer;

            //�}�i���\���ɂ��邩
            bool condition2 = h.Mana.Value >= playingCard.Cost.Value;

            //��ʂɃJ�[�h��z�u�ł��邩
            bool condition3 = PositionLocator.LI.Resolve<Field>(IsPlayer).CanAdd();

            if (!condition1 || !condition2 || !condition3)
            {
                Result = $"{(IsPlayer ? "�v���C���[" : "�ΐ푊��")}: {playingCard}�̃v���C�Ɏ��s";
                return;
            }
            else
            {
                //�v���C�ɗv����}�i���������Ă��邩
                if (h.ManaManager.TryUseMana(playingCard.Cost.Value))
                {
                    //�������Ă���ꍇ�̓J�[�h�̃|�W�V������ύX
                    if(h.TryMoveTo<Field>(indexTo, gameId))
                    {
                        RunAbility_OnPlay(playingCard);
                    }
                }

                Result = $"{(IsPlayer ? "�v���C���[" : "�ΐ푊��")}: {playingCard}���v���C";
            }
        }

        protected virtual void RunAbility_OnPlay(ICardInfo playingCard)
        {
            if (!(playingCard.AbilityBook is AbilityBook_Selectable))
            {
                ((IAbilityBase)playingCard.AbilityBook.AbilityGetter()).Run(playingCard);
            }
        }
    }
}
