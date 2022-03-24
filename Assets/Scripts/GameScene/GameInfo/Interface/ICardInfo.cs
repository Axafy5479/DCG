using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RX;

/// <summary>
/// �S�ẴJ�[�h�����ϐ��̂����A�ǂݎ���p�����݂̂𔲂��o�����C���^�[�t�F�[�X
/// </summary>
public interface ICardInfo
{
    /// <summary>
    /// �J�[�h�̖��O
    /// </summary>
    CardBook CardBook { get; }

    /// <summary>
    /// �J�[�h���z�u����Ă���ʒu
    /// </summary>
    IObservable<Pos> Pos { get; }

    /// <summary>
    /// ���݃v���C�\���ۂ� (��D�̃J�[�h�ł��g�p���邽�߁AIPlayable�ȊO�ł��K�v�ɂȂ鐫��)
    /// </summary>
    IObservable<bool> IsPlayable { get; }

    /// <summary>
    /// �v���C���[�̃J�[�h���ۂ�
    /// </summary>
    bool IsPlayer { get; }

    /// <summary>
    /// ���̃J�[�h�̎��
    /// </summary>
    CardType Type { get; }

    /// <summary>
    /// �Q�[�����̃J�[�h����肷��ID
    /// </summary>
    int GameId { get; }

    /// <summary>
    /// �v���C��Ability
    /// </summary>
    AbilityBook AbilityBook { get; }


    /// <summary>
    /// �v���C����ۂɏ����R�X�g
    /// </summary>
    /// <returns></returns>
    IObservable<int> Cost { get; }

    /// <summary>
    /// ����Pos�̉��Ԗڂɔz�u����Ă��邩
    /// </summary>
    int PositionIndex { get; }
}
