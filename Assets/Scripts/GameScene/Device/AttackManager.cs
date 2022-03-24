using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Position;

public class AttackManager
{
    /// <summary>
    /// gameId_aがgameId_tを攻撃
    /// </summary>
    /// <param name="gameId_a">アタッカーのGameId</param>
    /// <param name="gameId_t">ターゲットのGameId</param>
    public static void AttackCard(int gameId_a, int gameId_t,bool specialAttack)
    {
        CardStatus_Chara attacker;
        CardStatus_Chara target;

        var cards = new List<ICardInfo>(PositionLocator.LI.Resolve<Field>(true).Cards);
        cards.AddRange(new List<ICardInfo>(PositionLocator.LI.Resolve<Field>(false).Cards));

        attacker = cards.Find(c=>c.GameId == gameId_a) as CardStatus_Chara;
        target = cards.Find(c => c.GameId == gameId_t) as CardStatus_Chara;

        if(attacker == null || target == null)
        {
            throw new System.Exception($"gameid_a({gameId_a})またはgameId_t({gameId_t})のキャラクターカードが見つかりません");
        }

        StartBattle(attacker, target, specialAttack);
    }


    /// <summary>
    /// gameId_aがヒーローを攻撃
    /// </summary>
    /// <param name="gameId_a">アタッカーのGameId</param>
    /// <param name="gameId_t">ターゲットのGameId</param>
    public static void AttackHero(int gameId_a,bool specialAttack)
    {
        CardStatus_Chara attacker;
        HeroStatus target;

        var cards = new List<ICardInfo>(PositionLocator.LI.Resolve<Field>(true).Cards);
        cards.AddRange(new List<ICardInfo>(PositionLocator.LI.Resolve<Field>(false).Cards));

        attacker = cards.Find(c => c.GameId == gameId_a) as CardStatus_Chara;
        target = HeroLocator.HI.ResolveHero(!attacker.IsPlayer);

        if (attacker == null || target == null)
        {
            throw new System.Exception($"gameid_a({gameId_a})またはHeroが見つかりません");
        }

        StartBattle(attacker, target, specialAttack);
    }

    private static void StartBattle(CardStatus_Chara attacker, CardStatus_Chara target,bool specialAttack)
    {
        if (!specialAttack)
        {
            attacker.AttackNumDecrement();
        }
        target.Damage(attacker.Atk.Value);
        attacker.Damage(target.Atk.Value);
    }

    private static void StartBattle(CardStatus_Chara attacker, HeroStatus target,bool specialAttack)
    {
        if (!specialAttack)
        {
            attacker.AttackNumDecrement();
        }
        target.Damage(attacker.Atk.Value);
        attacker.Damage(target.Atk.Value);
    }
}
