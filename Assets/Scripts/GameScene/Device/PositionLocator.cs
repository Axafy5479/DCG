using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

namespace Position
{

    /// <summary>
    /// Position�̃C���X�^���X���擾���邽�߂̃T�[�r�X���P�[�^�N���X
    /// </summary>
    public class PositionLocator : PositionLocatorInfo
    {
        protected static PositionLocator instance;
        public static PositionLocator LI => instance ??= new PositionLocator();
        protected PositionLocator() { I = this; }

        /// <summary>
        /// �C���X�^���X��Type�̊֌W��o�^����
        /// </summary>
        /// <param name="isPlayer"></param>
        /// <param name="position"></param>
        public void Register(PositionBase position)
        {
            bool isPlayer = position.IsPlayer;
            var d = (isPlayer ? playerPositionMap : rivalPositionMap);

            if (d.ContainsKey(position.GetType()))
            {
                //���ڈȍ~�̃v���C�̏ꍇ�́A���O�̉e�����c���Ă��邽�ߍX�V
                d[position.GetType()] = position;
            }
            else
            {
                // �N���㏉�߂ăQ�[�����J�n����ꍇ��Add
                d.Add(position.GetType(), position);
            }
        }

        /// <summary>
        /// Position�N���X�̃C���X�^���X���擾����
        /// </summary>
        /// <typeparam name="T">�~�����C���X�^���X�̌^</typeparam>
        /// <param name="isPlayer">�v���C���[�̃C���X�^���X���ۂ�</param>
        /// <returns></returns>
        public T Resolve<T>(bool isPlayer) where T:PositionBase
        {
            string s = typeof(T).ToString();
            var p = playerPositionMap;
            return (isPlayer ? playerPositionMap : rivalPositionMap)[typeof(T)] as T;
        }

        /// <summary>
        /// �S�Ẵ|�W�V������Ԃ�
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
