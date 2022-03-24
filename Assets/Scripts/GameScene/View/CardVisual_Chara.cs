using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// デバッグ用のキャラカードの見た目
/// ステータスクラスから指示を受け、カードの状態を表示する
/// </summary>
public class CardVisual_Chara : CardVisual
{
    /// <summary>
    /// キャラのステータスを表示するテキストのGO
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
    /// キャラステータスをもとに表示を更新
    /// </summary>
    protected override void RX_Initialize()
    {
        base.RX_Initialize();

        //Atk変更が検出されたとき
        charaStatus.Atk.Subscribe(a =>
        {
            AnimationUtility.SetAtkAnimation(atk_text, charaStatus);
        });

        //Atk変更が検出されたとき
        charaStatus.Hp.Subscribe(h =>
        {
            AnimationUtility.SetHpAnimation(hp_text, charaStatus);
        });
    }


}
