using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public abstract class PositionView : MonoBehaviour
{
    [SerializeField] protected bool isPlayer;
    public bool IsPlayer => isPlayer;



    /// <summary>
    /// �L�����J�[�h�̃v���n�u�����݂���ꏊ
    /// </summary>
    protected virtual string PrefabPathForDebugging => "CharacterCard";

    public void Register()
    {
        PosViewLocator.I.Register(this);
    }

    public virtual void Initialize()
    {
        PositionLocatorInfo.I.Resolve(IsPlayer, Pos).CardMade.Subscribe(c =>
        {
            MakeCard(c);
        });
    }

    public abstract Pos Pos { get; }

    /// <summary>
    /// ���̃|�W�V�����ɃJ�[�h�̃Q�[���I�u�W�F�N�g�𐶐�����
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    private void MakeCard(ICardInfo status)
    {
        //�J�[�h�̃v���n�u���C���X�^���X��
        //�J�[�h�̎�ނɂ���āA�p����v���n�u���قȂ�
        GameObject cardObj = Instantiate(Resources.Load<GameObject>(PrefabPathForDebugging), transform, false);

        CardVisual debugVisual = cardObj.GetComponent<CardVisual>();

        //������₷���悤�ɁA�I�u�W�F�N�g���Ƃ��ăJ�[�h�̖��O��p����
        cardObj.name = status.CardBook.CardName;

        debugVisual.Initialize(status);
        int n = status.PositionIndex;
        cardObj.transform.SetSiblingIndex(n);

        if (n == 2)
        {
            cardObj.transform.SetAsFirstSibling();

        }
    }



}
