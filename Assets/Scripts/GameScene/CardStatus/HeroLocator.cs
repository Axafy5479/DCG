using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLocator : HeroLocatorInfo
{
    protected static HeroLocator instance;
    public static HeroLocator HI => instance ??= new HeroLocator();
    protected HeroLocator() { I = this; }

    /// <summary>
    /// HeroStatusを登録する
    /// </summary>
    /// <param name="isPlayer"></param>
    /// <param name="position"></param>
    public void Register(HeroStatus hero)
    {
        bool isPlayer = hero.IsPlayer;
        

        if (heroMap.ContainsKey(hero.IsPlayer))
        {
            //二回目以降のプレイの場合は、直前の影響が残っているため更新
            heroMap[hero.IsPlayer] = hero;
        }
        else
        {
            // 起動後初めてゲームを開始する場合はAdd
            heroMap.Add(hero.IsPlayer, hero);
        }
    }

    /// <summary>
    /// HeroStatusクラスのインスタンスを取得する
    /// </summary>
    /// <param name="isPlayer">プレイヤーのインスタンスか否か</param>
    /// <returns></returns>
    public HeroStatus ResolveHero(bool isPlayer)=> (HeroStatus)heroMap[isPlayer];

    public void Judge()
    {
        throw new System.NotImplementedException();
    }
}
