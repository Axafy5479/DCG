using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// �f�o�b�O�p�̃L�����J�[�h�̌�����
/// �X�e�[�^�X�N���X����w�����󂯁A�J�[�h�̏�Ԃ�\������
/// </summary>
public class CardVisual_Chara : CardVisual
{
    /// <summary>
    /// �L�����̃X�e�[�^�X��\������e�L�X�g��GO
    /// </summary>
    [SerializeField] private TextMeshProUGUI[] atk_text, hp_text;

    private IBattlerInfo charaStatus;

    protected override void View_Initialize()
    {
        base.View_Initialize();

        charaStatus = CardInfo as IBattlerInfo;
        atk_text.ForEach(t=>t.text = charaStatus.Atk.Value.ToString());
        hp_text.ForEach(t=>t.text = charaStatus.Hp.Value.ToString());
        //attacknum_text.text = charaStatus.AttackNumber.Value.ToString();
    }

    /// <summary>
    /// �L�����X�e�[�^�X�����Ƃɕ\�����X�V
    /// </summary>
    protected override void RX_Initialize()
    {
        base.RX_Initialize();

        //Atk�ύX�����o���ꂽ�Ƃ�
        charaStatus.Atk.Subscribe(a =>
        {
            AnimationUtility.SetAtkAnimation(atk_text, charaStatus);
        });

        //Atk�ύX�����o���ꂽ�Ƃ�
        charaStatus.Hp.Subscribe(h =>
        {
            AnimationUtility.SetHpAnimation(hp_text, charaStatus);
        });
    }


}
