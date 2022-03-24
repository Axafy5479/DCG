using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroLocator : HeroLocatorInfo
{
    protected static HeroLocator instance;
    public static HeroLocator HI => instance ??= new HeroLocator();
    protected HeroLocator() { I = this; }

    /// <summary>
    /// HeroStatus��o�^����
    /// </summary>
    /// <param name="isPlayer"></param>
    /// <param name="position"></param>
    public void Register(HeroStatus hero)
    {
        bool isPlayer = hero.IsPlayer;
        

        if (heroMap.ContainsKey(hero.IsPlayer))
        {
            //���ڈȍ~�̃v���C�̏ꍇ�́A���O�̉e�����c���Ă��邽�ߍX�V
            heroMap[hero.IsPlayer] = hero;
        }
        else
        {
            // �N���㏉�߂ăQ�[�����J�n����ꍇ��Add
            heroMap.Add(hero.IsPlayer, hero);
        }
    }

    /// <summary>
    /// HeroStatus�N���X�̃C���X�^���X���擾����
    /// </summary>
    /// <param name="isPlayer">�v���C���[�̃C���X�^���X���ۂ�</param>
    /// <returns></returns>
    public HeroStatus ResolveHero(bool isPlayer)=> (HeroStatus)heroMap[isPlayer];

    public void Judge()
    {
        throw new System.NotImplementedException();
    }
}
