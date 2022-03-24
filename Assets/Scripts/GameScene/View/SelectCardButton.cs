using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectCardButton : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private CanvasGroup cg;
    [SerializeField] private Image image;

    private ICardInfo CardInfo { get; set; } = null;

    public void Hide()
    {
        image.color = Color.white;
        cg.blocksRaycasts = false;
        cg.alpha = 0;
        CardInfo = null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (CardInfo != null)
        {
            SelectingManager.I.SelectedCard = CardInfo;
        }
    }

    public void Show(bool canSelect,ICardInfo cardInfo)
    {
        cg.blocksRaycasts = true;
        cg.alpha = 0.3f;
        image.color = canSelect? Color.red : Color.black;
        CardInfo = cardInfo;
    }
}
