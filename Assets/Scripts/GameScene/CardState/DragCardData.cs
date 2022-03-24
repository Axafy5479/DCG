using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayType
{
    Play,
    Attack
}

/// <summary>
/// D&D�ɂ�茈�肵��2�̏��(BeginCardId,DropData)��
/// ����2�̃��\�b�h(onDrop)�Ɏ󂯓n���N���X
/// �C���X�^���X��1�܂���0��
/// </summary>
public class DragCardData
{
    private static DragCardData instance;
    public static DragCardData I => instance ?? throw new Exception("�h���b�O���J�n����Ă��܂���");
    public static bool IsDragging => instance != null;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    /// <param name="beginCardId">�h���b�O�J�n�ɂ�蔻������J�[�h��id</param>
    /// <param name="onDrop"> ����2�̃��\�b�h</param>
    internal DragCardData(int beginCardId,Action<int,int> onDrop,PlayType playType)
    {
        if (IsDragging)
        {
            throw new Exception("�h���b�O���ł�");
        }
        instance = this;

        BeginCardId = beginCardId;
        OnDrop = onDrop;
        PlayType = playType;
    }

    /// <summary>
    /// �h���b�O�J�n�ɂ�蔻������J�[�h��id
    /// </summary>
    public int BeginCardId { get; }

    /// <summary>
    /// 2�ϐ����\�b�h
    /// (D&D�́A2�ϐ����\�b�h�̈��������肷�邽�߂̑���)
    /// </summary>
    private Action<int,int> OnDrop { get; }

    /// <summary>
    /// �h���b�v���ɔ�������f�[�^
    /// </summary>
    public int? DropData { get; private set; } = null;

    public PlayType PlayType { get; }

    /// <summary>
    /// �h���b�v���ɔ��������f�[�^���Z�b�g
    /// </summary>
    /// <param name="data"></param>
    public void SetDropData(int data)
    {
        DropData = data;
    }

    /// <summary>
    /// D&D�����������Ƃ��ɌĂ�
    /// �K�؂Ȉʒu�Ƀh���b�v����Ă���ꍇ�́A���炩���ߓo�^����Ă���2�ϐ����\�b�h���Ă�
    /// �h���b�O�t���O��false�ɂ���
    /// </summary>
    internal void OnEndDrag()
    {
        if (DropData is int DropDataInt)
        {
            OnDrop(BeginCardId, DropDataInt);
        }
        instance = null;
    }
}
