using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public　abstract class HeroLocatorInfo
{
    public static HeroLocatorInfo I { get; protected set; }

    protected Dictionary<bool, IHeroInfo> heroMap = new Dictionary<bool, IHeroInfo>();


    /// <summary>
    /// ヒーローのインスタンスを取得する
    /// </summary>
    /// <param name="isPlayer">プレイヤーor対戦相手</param>
    /// <returns>ヒーローのインスタンス</returns>
    public IHeroInfo ResolveIHero(bool isPlayer) => heroMap[isPlayer];
}
