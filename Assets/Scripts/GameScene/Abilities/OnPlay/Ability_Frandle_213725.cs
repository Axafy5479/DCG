using Position;

public class Ability_Frandle_213725 : IAbilityBase ,ISelectableAbility
{

    /// <summary>
    /// アビリティーの内容
    /// </summary>
    public void Run(ICardInfo owner)
    {
        ((CardStatus_Chara)PositionLocator.I.GetCardFromId(SelectedGameId)).AddStatusEffect(StatusEffect.Dead);

    }

    #region カードを選択して発動するスキルの場合に使用する
    /// <summary>
    /// 引数となるカードは選択可能かを判定する
    /// 
    /// 例:
    /// """atk5以上のカードを選んで破壊"""などが実現可能
    /// </summary>
    public static bool CheckSelectable(ICardInfo card)
    {
        return card is CardStatus_Chara;
    }

    /***********以下のコメントを外す***************/

    public int SelectedGameId { get; private set; }
    public void Set(int gameId)
    {
        SelectedGameId = gameId;
    }

    /******************************************/

    #endregion



    /// <summary>
    /// アビリティー名
    /// </summary>
    public string AbilityName => "フランドールのアビリティー";





    #region ※ 自動生成コード(変更厳禁) ※
    internal static AbilityBook GetAbilityBook()
    {
        if (typeof(Ability_Frandle_213725).GetInterface(typeof(ISelectableAbility).ToString()) != null)
        {
            return new AbilityBook_Selectable(() => new Ability_Frandle_213725(), true, 213725, CheckSelectable);
        }
        else
        {
            return new AbilityBook(() => new Ability_Frandle_213725(), false, 213725);
        }
    }

    /// <summary>
    /// アビリティー発動時、ターゲットとなるカードの選択が必要か否か
    /// </summary>
    public bool Selectable => this is ISelectableAbility;

    /// <summary>
    /// アビリティーID
    /// </summary>
    public int SpellId => 213725;
    #endregion




}