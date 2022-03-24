using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class CardDetailPanel : MonoSingleton<CardDetailPanel>,IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI cardNameText, descriptionText,cost_Txt,hp_text,atk_text;


    private CanvasGroup cg;
    public void OnPointerClick(PointerEventData eventData)
    {
        Hide();
    }

    // Start is called before the first frame update
    void Start()
    {
        cg = GetComponent<CanvasGroup>();
        Hide();
    }

    public void Show(ICardInfo info)
    {
        if(info.CardBook is CardBook_Chara chara)
        {
            cardNameText.text = chara.CardName;
            descriptionText.text = chara.Description;
            cost_Txt.text = chara.Cost.ToString();
            hp_text.text = chara.Hp.ToString();
            atk_text.text = chara.Atk.ToString();
        }

        cg.alpha = 1.0f;
        cg.blocksRaycasts = true;
    }

    public void Hide()
    {
        cg.alpha = 0;
        cg.blocksRaycasts = false;
    }
}
