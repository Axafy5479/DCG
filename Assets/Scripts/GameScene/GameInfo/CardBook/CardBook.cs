using System;
using UnityEngine;

/// <summary>
/// カードの種類
/// </summary>
public enum CardType
{
    Chara,
    Spell,
}

/// <summary>
/// カード図鑑(全てのカードに共通に存在する性質)
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
    /// カードのID (GameIdと区別するため、BookIdと呼ぶことにする)
    /// </summary>
    public int BookId { get => bookId; }

    /// <summary>
    /// カード名
    /// </summary>
    public string CardName { get => cardName; }

    /// <summary>
    /// カードの説明文
    /// </summary>
    public string Description { get => description; }

    /// <summary>
    /// カードプレイ時に消費するマナ
    /// </summary>
    public int Cost { get => cost; }

    /// <summary>
    /// カードの種類(キャラカード、スペルカードなど)
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
            throw new Exception("既にIDが付与されています");
        }
    }
}




