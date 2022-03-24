using Position;

public class Ability_Horai_976763 : IAbilityBase //,ISelectableAbility
{

    /// <summary>
    /// アビリティーの内容
    /// </summary>
    public void Run(ICardInfo owner)
    {
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
        return true;
    }

    /***********以下のコメントを外す***************/

    //public int SelectedGameId { get; private set; }
    //public void Set(int gameId)
    //{
    //    SelectedGameId = gameId;
    //}

    /******************************************/

    #endregion



    /// <summary>
    /// アビリティー名
    /// </summary>
    public string AbilityName => "蓬莱人形のアビリティー";





    #region ※ 自動生成コード(変更厳禁) ※
    internal static AbilityBook GetAbilityBook()
    {
        if (typeof(Ability_Horai_976763).GetInterface(typeof(ISelectableAbility).ToString()) != null)
        {
            return new AbilityBook_Selectable(() => new Ability_Horai_976763(), true, 976763, CheckSelectable);
        }
        else
        {
            return new AbilityBook(() => new Ability_Horai_976763(), false, 976763);
        }
    }

    /// <summary>
    /// アビリティー発動時、ターゲットとなるカードの選択が必要か否か
    /// </summary>
    public bool Selectable => this is ISelectableAbility;

    /// <summary>
    /// アビリティーID
    /// </summary>
    public int SpellId => 976763;
    #endregion




}