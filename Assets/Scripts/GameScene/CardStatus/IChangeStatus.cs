using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L�����N�^�[�J�[�h����������C���^�[�t�F�[�X
/// </summary>
public interface IChangeStatus: IBattlerInfo
{
    /// <summary>
    /// ���S���ɌĂ΂�郁�\�b�h
    /// </summary>
    public void OnDead();

    /// <summary>
    /// Hp�����������郁�\�b�h
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public int Damage(int damage);

    /// <summary>
    /// ��Ԉُ�̒ǉ�
    /// </summary>
    /// <param name="statusEffect"></param>
    public void AddStatusEffect(StatusEffect statusEffect);

    /// <summary>
    /// ��Ԉُ�̏���
    /// </summary>
    /// <param name="statusEffect"></param>
    public void RemoveStatusEffect(StatusEffect statusEffect);
}
