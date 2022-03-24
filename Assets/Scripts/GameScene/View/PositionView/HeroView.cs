using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class HeroView : MonoBehaviour,IDropHandler,IPointerClickHandler
{
    [SerializeField] private bool isPlayer;
    [SerializeField] private int maxHp = 20;
    [SerializeField] private GameObject priventInputPanel;
    [SerializeField] private TextMeshProUGUI hpText;


    public int MaxHp { get => maxHp;  }
    public bool IsPlayer { get => isPlayer; }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragCardData.IsDragging && DragCardData.I.PlayType == PlayType.Attack)
        {
            if (PositionLocatorInfo.I.Resolve(!IsPlayer, Pos.Field).Cards.Any(c => c.GameId == DragCardData.I.BeginCardId))
            {
                DragCardData.I.SetDropData(-1);
            }
        }
    }

    public void Initialize()
    {
        priventInputPanel.SetActive(false);
        HeroLocatorInfo.I.ResolveIHero(IsPlayer).Hp.Subscribe(hp => { 
        
            if(hp <= 0)
            {
                //ユーザーの入力を禁止する
                priventInputPanel.SetActive(true);

                //ゲーム終了アニメーションを登録
                AnimationUtility.GameSet(!IsPlayer);

                //これ以上アニメーションは追加しない
                AnimationQueue.I.BlockAddQueue();
            }

        });

        var hero = HeroLocatorInfo.I.ResolveIHero(isPlayer);
        hero.Hp.Subscribe(hp => hpText.text = hp.ToString());
        hpText.text = hpText.text = hero.Hp.Value.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }
}
