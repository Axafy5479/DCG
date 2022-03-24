using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// enum Pos ����IPosition�̃C���X�^���X���擾�ł���T�[�r�X���P�[�^�N���X
/// IPosition�́APositionBase�N���X�̂����ǂݎ���p�ϐ��̂݌��J����C���^�[�t�F�[�X
/// </summary>
public abstract class PositionLocatorInfo
{
    public static PositionLocatorInfo I { get; protected set; }

    /// <summary>
    /// �v���C���[��Position�N���X
    /// </summary>
    protected Dictionary<Type, IPosition> playerPositionMap = new Dictionary<Type, IPosition>();

    /// <summary>
    /// �ΐ푊���Position�N���X
    /// </summary>
    protected Dictionary<Type, IPosition> rivalPositionMap = new Dictionary<Type, IPosition>();

    /// <summary>
    /// �|�W�V�����̃C���X�^���X���擾����
    /// </summary>
    /// <param name="isPlayer">�v���C���[or�ΐ푊��</param>
    /// <param name="pos">�ǂ̃|�W�V�����̃C���X�^���X���~������</param>
    /// <returns>�|�W�V�����̃C���X�^���X</returns>
    public IPosition Resolve(bool isPlayer, Pos pos)
    {
        var d = isPlayer ? playerPositionMap : rivalPositionMap;
        return d.Values.FindFirst(position => position.Pos == pos);
    }

    public ICardInfo GetCardFromId(int id)
    {
        return GetCardFromId(id, true) ?? GetCardFromId(id, false);
    }

    public ICardInfo GetCardFromId(int id, bool isPlayer)
    {
        ICardInfo cardInfo = null;
        var d = isPlayer ? playerPositionMap : rivalPositionMap;
        foreach (var item in d)
        {
            cardInfo = item.Value.Cards.FindFirst(c => c.GameId == id);
            if (cardInfo != null) break;
        }
        return cardInfo;
    }

    public List<ICardInfo> GetPlayableCards(bool isPlayer, Pos pos)
    {
        List<ICardInfo> cards = new List<ICardInfo>();
        foreach (var item in Resolve(isPlayer,pos).Cards)
        {
            if (item.IsPlayable.Value)
            {
                cards.Add(item);
            }
        }
        return cards;
    }

}