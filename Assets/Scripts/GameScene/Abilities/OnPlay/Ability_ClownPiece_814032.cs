using Position;
using System.Linq;

public class Ability_ClownPiece_814032 : IAbilityBase //,ISelectableAbility
{

    /// <summary>
    /// アビリティーの内容
    /// </summary>
    public void Run(ICardInfo owner)
    {
        var charas = PositionLocator.LI.Resolve<Field>(!owner.IsPlayer).Cards.ConvertType<IBattlerInfo>().NonNull().ToList();

        if (charas.Count > 1)
        {
            charas.Sort((a, b) => a.Atk.Value - b.Atk.Value);

            Field.Attack(charas[0].GameId,charas[1].GameId,true);
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
    public string AbilityName => "クラウンピースのアビリティー";





    #region ※ 自動生成コード(変更厳禁) ※
    internal static AbilityBook GetAbilityBook()
    {
        if (typeof(Ability_ClownPiece_814032).GetInterface(typeof(ISelectableAbility).ToString()) != null)
        {
            return new AbilityBook_Selectable(() => new Ability_ClownPiece_814032(), true, 814032, CheckSelectable);
        }
        else
        {
            return new AbilityBook(() => new Ability_ClownPiece_814032(), false, 814032);
        }
    }

    /// <summary>
    /// アビリティー発動時、ターゲットとなるカードの選択が必要か否か
    /// </summary>
    public bool Selectable => this is ISelectableAbility;

    /// <summary>
    /// アビリティーID
    /// </summary>
    public int SpellId => 814032;
    #endregion




}