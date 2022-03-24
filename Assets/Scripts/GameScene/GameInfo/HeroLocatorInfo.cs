using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public�@abstract class HeroLocatorInfo
{
    public static HeroLocatorInfo I { get; protected set; }

    protected Dictionary<bool, IHeroInfo> heroMap = new Dictionary<bool, IHeroInfo>();


    /// <summary>
    /// �q�[���[�̃C���X�^���X���擾����
    /// </summary>
    /// <param name="isPlayer">�v���C���[or�ΐ푊��</param>
    /// <returns>�q�[���[�̃C���X�^���X</returns>
    public IHeroInfo ResolveIHero(bool isPlayer) => heroMap[isPlayer];
}
