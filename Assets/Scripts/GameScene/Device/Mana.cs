using RX;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// �}�i�̋@�\����������N���X
    /// </summary>
    public class Mana
    {
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        /// <param name="manaText"></param>
        public Mana()
        {
            CurrentMana = new Subject<int>(0);
            InitialMana = 0;
        }

        /// <summary>
        /// ���ݏ������Ă���}�i
        /// </summary>
        public Subject<int> CurrentMana
        {
            get => currentMana;
            protected set
            {
                currentMana = value;
            }
        }
        private Subject<int> currentMana;

        /// <summary>
        /// �}�i��1�^�[����1���㏸���邪�A10�𒴂��ď㏸���Ȃ�
        /// </summary>
        private int LimitMana => 10;

        /// <summary>
        /// �^�[�����߂̃}�i�̗�(1�^�[����1���㏸)
        /// </summary>
        private int InitialMana { get; set; }

        /// <summary>
        /// �������̃}�i������邩���m���߁A
        /// ����ł���ꍇ�͏����true��Ԃ�
        /// �s�\�Ȃ�Ώ������false��Ԃ�
        /// </summary>
        /// <param name="cost"></param>
        /// <returns></returns>
        public bool TryUseMana(int cost)
        {
            if (CurrentMana.Value < cost)
            {
                return false;
            }
            else
            {
                CurrentMana.OnNext(CurrentMana.Value- cost);
                return true;
            }
        }

        /// <summary>
        /// �^�[���J�n���ɌĂ�
        /// initialMana��1���₵�A�}�i��S�񕜂���
        /// </summary>
        public void NewTurn()
        {
            InitialMana = Mathf.Min(LimitMana, InitialMana + 1);
            CurrentMana.OnNext(InitialMana);
        }
    }

    /// <summary>
    /// �f�o�b�O�@�\��������}�i
    /// </summary>
    public class Mana_Debug : Mana
    {

        /// <summary>
        /// �����}�i�������I��1���₷
        /// </summary>
        public void AddCurrrentMana()
        {
            CurrentMana.OnNext(CurrentMana.Value+1);
        }
    }
}