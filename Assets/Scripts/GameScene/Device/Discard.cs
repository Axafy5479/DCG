using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Position
{
    /// <summary>
    /// �̂ĎD�̋@�\����������N���X
    /// </summary>
    public class Discard : PositionBase
    {
        public Discard(bool isPlayer) : base(isPlayer)
        {
        }

        public override Pos Pos => Pos.Discard;

        /// <summary>
        /// �̂ĎD�ɂ͉����ł��J�[�h��ǉ��\
        /// </summary>
        /// <returns></returns>
        public override bool CanAdd() => true;

    }
}
