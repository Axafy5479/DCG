using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardDataBaseInfo
{
    public static CardDataBaseInfo I { get; protected set; }

    protected Dictionary<int, CardBook> CardBook { get; set; }


    /// <summary>
    /// �q�[���[�̃C���X�^���X���擾����
    /// </summary>
    /// <param name="isPlayer">�v���C���[or�ΐ푊��</param>
    /// <returns>�q�[���[�̃C���X�^���X</returns>
    public CardBook GetBook(int id) => CardBook[id];
}
