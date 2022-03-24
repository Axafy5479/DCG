using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �J�[�h�̈ړ����\�ȃC���^�[�t�F�[�X
/// </summary>
public interface ICardMover : ICardInfo
{
    /// <summary>
    /// �J�[�h�̃X�e�[�^�X��Pos��ύX����
    /// </summary>
    /// <param name="posTo"></param>
    void ChangePos(Pos posTo);

    /// <summary>
    /// �ړ��\����ύX����
    /// </summary>
    /// <param name="canMove"></param>
    void ChangePlayable(bool canMove);
}
