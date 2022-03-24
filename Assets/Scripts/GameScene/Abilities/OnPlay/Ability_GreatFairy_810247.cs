using Position;
using System.Linq;

public class Ability_GreatFairy_810247 : IAbilityBase //,ISelectableAbility
{

    /// <summary>
    /// アビリティーの内容
    /// </summary>
    public void Run(ICardInfo owner)
    {
        var charas = PositionLocator.LI.Resolve<Field>(owner.IsPlayer).Cards.ConvertType<CardStatus_Chara>().NonNull().Where(c=>c.Cost.Value<=2).ToList();

        foreach (var c in charas)
        {
            c.ChangeAtk(1);
            c.AddHp(1);
        }
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
    public string AbilityName => "大妖精のアビリティー";





    #region ※ 自動生成コード(変更厳禁) ※
    internal static AbilityBook GetAbilityBook()
    {
        if (typeof(Ability_GreatFairy_810247).GetInterface(typeof(ISelectableAbility).ToString()) != null)
        {
            return new AbilityBook_Selectable(() => new Ability_GreatFairy_810247(), true, 810247, CheckSelectable);
        }
        else
        {
            return new AbilityBook(() => new Ability_GreatFairy_810247(), false, 810247);
        }
    }

    /// <summary>
    /// アビリティー発動時、ターゲットとなるカードの選択が必要か否か
    /// </summary>
    public bool Selectable => this is ISelectableAbility;

    /// <summary>
    /// アビリティーID
    /// </summary>
    public int SpellId => 810247;
    #endregion




}