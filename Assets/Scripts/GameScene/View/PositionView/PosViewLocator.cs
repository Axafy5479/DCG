using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosViewLocator
{
    private static PosViewLocator instance;
    public static PosViewLocator I => instance ??= new PosViewLocator();
    private PosViewLocator() { }



    /// <summary>
    /// �v���C���[��Position�N���X
    /// </summary>
    private Dictionary<Type, PositionView> playerPositionMap = new Dictionary<Type, PositionView>();

    /// <summary>
    /// �ΐ푊���Position�N���X
    /// </summary>
    private Dictionary<Type, PositionView> rivalPositionMap = new Dictionary<Type, PositionView>();

    /// <summary>
    /// �C���X�^���X��Type�̊֌W��o�^����
    /// </summary>
    /// <param name="isPlayer"></param>
    /// <param name="position"></param>
    public void Register(PositionView position)
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
    public T Resolve<T>(bool isPlayer) where T : PositionView
    {
        return (isPlayer ? playerPositionMap : rivalPositionMap)[typeof(T)] as T;
    }

    public PositionView Resolve(bool isPlayer, Pos pos)
    {
        var d = isPlayer ? playerPositionMap : rivalPositionMap;

        foreach (var item in d)
        {
            if (item.Value.Pos == pos) return item.Value;
        }
        return null;
    }

}
