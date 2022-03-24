using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RX;

namespace GameInfo
{
    /// <summary>
    /// ���݂ǂ���̃^�[���ł��邩�̏���ێ������N���X�B
    /// TurnManager  --|>  TurnInfo
    /// �̊֌W�ƂȂ��Ă���ATurnManager�̓V���O���g���B
    /// TurnManager�̃C���X�^���X���������ꂽ�^�C�~���O�� TurnInfo.I �ɒl������
    /// </summary>
    public abstract class TurnInfo
    {
        /// <summary>
        /// �p����ł���TurnManager�̃C���X�^���X���������ꂽ�^�C�~���O��
        /// �C���X�^���X����������
        /// </summary>
        private static TurnInfo instance;

        /// <summary>
        /// TurnInfo�̃C���X�^���X
        /// </summary>
        public static TurnInfo I 
        {
            get
            {
                if (instance == null)
                {
                    //TurnManager�̃C���X�^���X�𐶐�����܂ł́A
                    //instance��null�ƂȂ��Ă���
                    throw new System.Exception("���TurnManager�̃C���X�^���X�𐶐����Ă�������");
                }
                return instance;
            } 
            protected set => instance = value; 
        }

        /// <summary>
        /// ���݂ǂ���̃^�[����
        /// </summary>
        public IObservable<bool> Turn { get; protected set; }
    }
}
