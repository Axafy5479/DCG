using RX;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

/// <summary>
/// �J�[�h��z�u�ł���ʒu
/// </summary>
public enum Pos
{
    Deck,
    Hand,
    Field,
    Discard,
    Hero
}

/// <summary>
/// PositionBase�̕ϐ��̂����A
/// �ǂݎ���p�ȕϐ��̂݌��J����C���^�[�t�F�[�X
/// </summary>
public interface IPosition
{
    /// <summary>
    /// ���݂���J�[�h
    /// </summary>
    ReadOnlyCollection<ICardInfo> Cards { get; }

    /// <summary>
    /// �v���C���[or�ΐ푊��
    /// </summary>
    bool IsPlayer { get; }

    /// <summary>
    /// �ǂ̃|�W�V������
    /// </summary>
    Pos Pos { get; }

    IObservable<ICardInfo> CardMade { get; }
}

