using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using RX;

/// <summary>
/// �L�����N�^�[�J�[�h�����ϐ��̂����A�ǂݎ���p�����̂ݔ����o�����C���^�[�t�F�[�X
/// </summary>
public interface IBattlerInfo: ICardInfo
{
    /// <summary>
    /// �c��̍U���\��
    /// </summary>
    public IObservable<int> AttackNumber { get; }

    /// <summary>
    /// Hp�̒l
    /// </summary>
    public IObservable<int> Hp { get; }//=> HpData;

    /// <summary>
    /// Atk�̒l
    /// </summary>
    public IObservable<int> Atk { get; }//=> HpData;

    /// <summary>
    /// �������Ă����Ԉُ�
    /// </summary>
    public ReadOnlyCollection<StatusEffect> StatusEffects { get; }
}
