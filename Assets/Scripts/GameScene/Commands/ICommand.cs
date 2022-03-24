using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Position;

namespace Command
{
    /// <summary>
    /// �f�o�b�O���ɂ̂ݎg�p�\�ȃR�}���h����������C���^�[�t�F�[�X
    /// </summary>
    public interface ICommandForDebugging { }

    /// <summary>
    /// ���ׂẴR�}���h�N���X�͂��̒��ۃN���X���p������
    /// </summary>
    public abstract class CommandBase
    {
        /// <summary>
        /// �v���C���[�̃R�}���h���ۂ�
        /// </summary>
        public bool IsPlayer => isPlayer;
        [SerializeField] private bool isPlayer;

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="isPlayer"></param>
        public CommandBase(bool isPlayer)
        {
            this.isPlayer = isPlayer;
        }

        internal void Execute()
        {
            _execute();
            PositionLocator.LI.Judge();
        }

        /// <summary>
        /// �R�}���h�����s����
        /// </summary>
        protected abstract void _execute();

        /// <summary>
        /// �R�}���h�̖��O
        /// </summary>
        public string Result { get; protected set; } = "�R�}���h���s�O";
    }


    [System.Serializable]
    public class Command_Draw : CommandBase, ICommandForDebugging
    {
        public Command_Draw(bool isPlayer) : base(isPlayer) { }

        protected override void _execute()
        {
            ICardInfo card = PositionLocator.LI.Resolve<Deck>(IsPlayer).Draw();
            Result = $"[ForDebugging]{(IsPlayer ? "�v���C���[" : "����")}: {card}���h���[";
        }
    }

    [System.Serializable]
    public class Command_AddMana :CommandBase, ICommandForDebugging
    {
        public Command_AddMana(bool isPlayer):base(isPlayer)
        {
        }

        protected override void _execute()
        {
            if(PositionLocator.LI.Resolve<Hand>(IsPlayer).ManaManager is Mana_Debug mana)
            {
                mana.AddCurrrentMana();
                Result = $"[ForDebugging]{(IsPlayer ? "�v���C���[" : "����")}: �}�i��1�ǉ�";
            }
        }
    }
}