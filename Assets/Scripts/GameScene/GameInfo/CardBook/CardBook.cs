using System;
using UnityEngine;

/// <summary>
/// �J�[�h�̎��
/// </summary>
public enum CardType
{
    Chara,
    Spell,
}

/// <summary>
/// �J�[�h�}��(�S�ẴJ�[�h�ɋ��ʂɑ��݂��鐫��)
/// </summary>
public abstract class CardBook
{
    protected CardBook(int bookId, string cardName, string name_en, string description, int cost, int abilityId)
    {
        this.bookId = bookId;
        this.cardName = cardName;
        this.description = description;
        this.cost = cost;
        this.abilityId = abilityId;
        this.name_en = name_en;
    }

    [SerializeField] private int bookId;
    [SerializeField] private string cardName;
    [SerializeField] private string description;
    [SerializeField] private int cost;
    [SerializeField] private CardType type;
    [SerializeField] private int abilityId = -1;
    [SerializeField] private string name_en;

    /// <summary>
    /// �J�[�h��ID (GameId�Ƌ�ʂ��邽�߁ABookId�ƌĂԂ��Ƃɂ���)
    /// </summary>
    public int BookId { get => bookId; }

    /// <summary>
    /// �J�[�h��
    /// </summary>
    public string CardName { get => cardName; }

    /// <summary>
    /// �J�[�h�̐�����
    /// </summary>
    public string Description { get => description; }

    /// <summary>
    /// �J�[�h�v���C���ɏ����}�i
    /// </summary>
    public int Cost { get => cost; }

    /// <summary>
    /// �J�[�h�̎��(�L�����J�[�h�A�X�y���J�[�h�Ȃ�)
    /// </summary>
    public abstract CardType CardType { get; }

    public string Name_en => name_en;

    public int AbilityId => abilityId;

    public void SetSkillId(int id)
    {
        if (abilityId == -1)
        {
            abilityId = id;
        }
        else
        {
            throw new Exception("����ID���t�^����Ă��܂�");
        }
    }
}




