using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameId�𐶐�����N���X
/// �S�ẴJ�[�h�̃C���X�^���XID�Ƃ��Ďg�p
/// (�I�����C���ΐ�̍ہA�����J�[�h�ɂ͓���ID���t�^�����)
/// </summary>
public class GameCardID:MonoSingleton<GameCardID>
{
    /// <summary>
    /// GameId��BookId��R�Â���
    /// </summary>
    private Dictionary<int, int> GameId_BookId_Map = new Dictionary<int, int>();

    /// <summary>
    /// �ʐM�ΐ푊��ɁA���g��GameId�Ɩ���������Id��n�����߂�Queue
    /// </summary>
    private Queue<(int, int)> GameId_BookId_Queue = new Queue<(int,int)>();

    /// <summary>
    /// ����GetId���Ă΂ꂽ�ۂɓn��Id
    /// 1�Ԃ����тɃC���N�������g
    /// </summary>
    private int NextId
    {
        get
        {
            int id = nextId;
            nextId++;
            return id;
        }
    }
    private int nextId = 1;


    /// <summary>
    /// GameId�𔭍s����
    /// </summary>
    /// <param name="bookId"></param>
    /// <returns></returns>
    public int GetId(int bookId)
    {
        int id = NextId;

        //GameId��BoookId�̊֌W��ǉ�
        GameId_BookId_Map.Add(id, bookId);

        //Queue�ɂ����l�̃y�A��ǉ�
        GameId_BookId_Queue.Enqueue((id, bookId));

        return id;
    }


}
